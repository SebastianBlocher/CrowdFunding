using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using System;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class RewardService : IRewardService
    {
        private CrowdFundingDbContext context_;        
        public RewardService(CrowdFundingDbContext context)
        {
            context_ = context;            
        }

        public Result<Reward> CreateReward(int rewardPackageId,
            CreateRewardOptions options)
        {
            if (options == null)
            {
                return Result<Reward>.ActionFailed(
                    StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Name))
            {
                return Result<Reward>.ActionFailed(
                    StatusCode.BadRequest, "Null or empty Name");
            }

            var rewardPackage = context_
                .Set<RewardPackage>()
                .Where(rp => rp.RewardPackageId == rewardPackageId)
                .SingleOrDefault();
                

            if (rewardPackage == null)
            {
                return Result<Reward>.ActionFailed(
                    StatusCode.BadRequest, "Invalid Reward Package");
            }

            var reward = new Reward()
            {
                Name = options.Name,
                Description = options.Description,
                
            };

            context_.Add(reward);

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<Reward>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<Reward>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Reward could not be created");
            }

            return Result<Reward>.ActionSuccessful(reward);
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
                query = query.Where(r => r.RewardId == options.RewardId.Value);
            }

            query = query.Take(500);

            return query;
        }

        public Result<Reward> UpdateReward(
        int rewardId,
        UpdateRewardOptions options)
        {
            var result = new Result<Reward>();

            if (options == null)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null options";

                return result;
            }

            var reward = SearchReward(new SearchRewardOptions()
            {
                RewardId = rewardId
            }).SingleOrDefault();

            if (reward == null)
            {
                result.ErrorCode = StatusCode.NotFound;
                result.ErrorText = $"Reward with id {rewardId} was not found";

                return result;
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                reward.Name = options.Name;
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                reward.Description = options.Description;
            }

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<Reward>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<Reward>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Reward could not be updated");
            }

            return Result<Reward>.ActionSuccessful(reward);
        }
    }
}
