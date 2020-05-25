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
        private IProjectService projectService_;

        public RewardPackageService(
            CrowdFundingDbContext context,
            IRewardService rewardService,
            IProjectService projectService)
        {
            context_ = context;
            rewardService_ = rewardService;
            projectService_ = projectService;
        }

        public Result<RewardPackage> CreateRewardPackage(
            int projectId,
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

            if (options.Amount <= 0)
            {
                return Result<RewardPackage>.ActionFailed(
                  StatusCode.BadRequest, "Null options");
            }           

            var project = projectService_.GetProjectById(projectId).SingleOrDefault();

            if (project == null)
            {
                return Result<RewardPackage>.ActionFailed(
                  StatusCode.BadRequest, "Null options");
            }

            var rewardPackage = new RewardPackage()
            {
                Amount = options.Amount,
                Description = options.Description,
                Name = options.Name
            };    

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

            //----------------------
            //var project = projectService_.SearchProject(new SearchProjectOption()
            //{
            //    ProjectId = options.ProjectId

            //}).SingleOrDefault();

            //if (project != null)
            //{
            //    query = query.Where(rp => rp.ProjectId == options.ProjectId);
            //}
            //-----------------------
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
            }).SingleOrDefault();

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
