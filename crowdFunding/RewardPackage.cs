using crowdFunding.Options;
using System;
using System.Collections.Generic;

namespace crowdFunding
{
    public class RewardPackage
    {
        public List<Reward> Rewards { get; set; }
        public string Name { get; set; }
        public int RewardPackageId { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public RewardPackage()
        {
            Rewards = new List<Reward>();
        }

        //public static implicit operator RewardPackage(CreateRewardOptions v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
