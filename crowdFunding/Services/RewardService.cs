using crowdFunding.Options;
using System.Linq;

namespace crowdFunding.Services
{
    public class RewardService : IRewardService
    {
        private CrowdFundingDbContext context_;
        public RewardService(CrowdFundingDbContext context)
        {
            context_ = context;
        }

        public Reward CreateReward(
            CreateRewardOptions options)
        {
            if (options == null || string.IsNullOrWhiteSpace(options.Name))                 
            {
                return null;
            }

            var reward = new Reward()
            {
                Name = options.Name,
                Description = options.Description              
            };

            context_.Add(reward);

            if (context_.SaveChanges() > 0)
            {
                return reward;
            }

            return null;
        }

        public Reward GetRewardById(int? rewardId)
        {
            if (rewardId == null)
            {
                return null;
            }

            var reward = SearchReward(new SearchRewardOptions()
            {
                RewardId = rewardId
            }).SingleOrDefault();

            return reward;
        }

        public bool RemoveReward(int? rewardId)
        {
            if (rewardId == null)
            {
                return false;
            }

            var reward = GetRewardById(rewardId);

            if (reward == null)
            {
                return false;
            }

            context_.Remove(reward);

            if (context_.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public IQueryable<Reward> SearchReward(
            SearchRewardOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context_
                .Set<Reward>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(r => r.Name == options.Name);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(r => r.Description == options.Description);
            }

            if (options.RewardId != null)
            {
                query = query.Where(r => r.RewardId == options.RewardId);
            }

            query = query.Take(500);

            return query;
        }

        Reward IRewardService.UpdateReward(UpdateRewardOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var reward = SearchReward(new SearchRewardOptions()
            {
                RewardId = options.RewardId
            }).SingleOrDefault();

            if (reward == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                reward.Name = options.Name;
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                reward.Description = options.Description;
            } 

            if (context_.SaveChanges() > 0)
            {
                return reward;
            }

            return null;
        }
    }
}
