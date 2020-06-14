using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class RewardPackageService : IRewardPackageService
    {
        private CrowdFundingDbContext context_;
        private IRewardService rewardService_;        

        public RewardPackageService(
            CrowdFundingDbContext context,
            IRewardService rewardService)            
        {
            context_ = context;
            rewardService_ = rewardService;            
        }

        public Result<RewardPackage> CreateRewardPackage(
            CreateRewardPackageOptions options)
        {
            if (options == null)
            {
                return Result<RewardPackage>.ActionFailed(
                    StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Description))
            {
                return Result<RewardPackage>.ActionFailed(
                  StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Name))
            {
                return Result<RewardPackage>.ActionFailed(
                  StatusCode.BadRequest, "Null options");
            }

            if (options.Amount == null || options.Amount <= 0)
            {
                return Result<RewardPackage>.ActionFailed(
                  StatusCode.BadRequest, "Invalid Amount");
            }

            var project = context_
               .Set<Project>()
               .Where(p => p.ProjectId == options.ProjectId)
               .SingleOrDefault();

            if (project == null)
            {
                return Result<RewardPackage>.ActionFailed(
                  StatusCode.BadRequest, "Invalid ProjectId");
            }

            var rewardPackage = new RewardPackage()
            {
                Amount = options.Amount,
                Description = options.Description,
                Name = options.Name
            };

            foreach (var option in options.RewardOptions)
            {
                if (option == null)
                {
                    continue;
                }

                var createdReward = rewardService_.AddRewardToList(option);

                if (createdReward != null)
                {
                    rewardPackage.Rewards.Add(createdReward.Data);
                }
                else
                {
                    return Result<RewardPackage>.ActionFailed(
                    StatusCode.BadRequest, "Invalid Rewards given");
                }
            }
                        
            project.RewardPackages.Add(rewardPackage);
            context_.Add(rewardPackage);

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<RewardPackage>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<RewardPackage>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Reward Package could not be created");
            }

            return Result<RewardPackage>.ActionSuccessful(rewardPackage);
        }

        public IQueryable<RewardPackage> SearchRewardPackage(SearchRewardPackageOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context_
                .Set<RewardPackage>()                                
                .AsQueryable();

            if (options.RewardPackageId != null)
            {
                query = query.Where(rp => rp.RewardPackageId == options.RewardPackageId.Value);
            }

            if (options.Amount != null)
            {
                query = query.Where(rp => rp.Amount == options.Amount.Value);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(rp => rp.Description == options.Description);
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(rp => rp.Name == options.Name);
            }

            query = query
                .Include(x => x.Rewards)
                .Take(500);

            return query;
        }

        public Result<RewardPackage> UpdateRewardPackage(
            int rewardPackageId,
            UpdateRewardPackageOptions options)
        {
            var result = new Result<RewardPackage>();

            if (options == null)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null options";

                return result;
            }

            var rewardPackage = GetRewardPackageById(rewardPackageId);

            if (rewardPackage == null)
            {
                result.ErrorCode = StatusCode.NotFound;
                result.ErrorText = $"Reward Package with id {rewardPackageId} was not found";

                return result;
            }

            if (options.Amount != null)
            {
                rewardPackage.Amount = options.Amount;
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                rewardPackage.Description = options.Description;
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                rewardPackage.Name = options.Name;
            }            
            
            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<RewardPackage>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<RewardPackage>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Reward Package could not be updated");
            }

            return Result<RewardPackage>.ActionSuccessful(rewardPackage);
        }

        public RewardPackage GetRewardPackageById(int? rewardPackageId)
        {
            if (rewardPackageId == null)
            {
                return null;
            }

            var rewardPackage = SearchRewardPackage(new SearchRewardPackageOptions()
            {
                RewardPackageId = rewardPackageId
            }).Include(rp => rp.Rewards)
            .SingleOrDefault();

            return rewardPackage;
        }

        public bool RemoveRewardPackage(int? rewardPackageId)
        {
            if (rewardPackageId == null)
            {
                return false;
            }

            var rewardPackage = GetRewardPackageById(rewardPackageId);

            if (rewardPackage == null)
            {
                return false;
            }

            foreach (var reward in rewardPackage.Rewards.ToList())
            {
                if (!rewardService_.RemoveReward(reward.RewardId))
                {
                    return false;
                }
            }

            context_.Remove(rewardPackage);

            if (context_.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
