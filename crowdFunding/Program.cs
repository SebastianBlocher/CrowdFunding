using System;
using System.Linq;

namespace crowdFunding
{
    class Program
    {
        static void Main(string[] args)
        {
            var crowdDb = new CrowdFundingDbContext();

            var backer = new Backer()
            {
                FirstName = "mema",
                Amount = 300m
            };
            crowdDb.Add(backer);

            var reward = new Reward()
            {
                Name = "1st reward",
            };

            var rewardPackage = new RewardPackage()
            {
                Ammount = 50,
            };
            rewardPackage.Reward.Add(reward);

            var backRewPack = new BackerRewardPackage()
            {
                Backer = backer,
                RewardPackage = rewardPackage
            };
            crowdDb.Add(backRewPack);

            var p1 = crowdDb.Set<Project>().Where(p => p.ProjectId == 1).SingleOrDefault();
            p1.Rewards.Add(rewardPackage);

            var proBack = new ProjectBacker()
            {
                Backer = backer,
                Project = p1,
            };
            crowdDb.Add(proBack);

            crowdDb.SaveChanges();
        }
    }
}
