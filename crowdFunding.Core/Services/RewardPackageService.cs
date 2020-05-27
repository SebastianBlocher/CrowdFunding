using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.EntityFrameworkCore;
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

        public RewardPackage CreateRewardPackage(
            CreateRewardPackageOptions options)
        {
            if (options == null 
                || options.Amount == null 
                || string.IsNullOrWhiteSpace(options.Description) 
                || string.IsNullOrWhiteSpace(options.Name))
            {
                return null;
            }

            var project = projectService_.SearchProject(new SearchProjectOptions()
            {
                ProjectId = options.ProjectId

            }).SingleOrDefault();

            if (project == null)
            {
                return null;
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

                var createdReward = rewardService_.CreateReward(option);

                if (createdReward != null)
                {                 
                    rewardPackage.Rewards.Add(createdReward);
                }
                else
                {
                    return null;
                }
            }

            project.RewardPackages.Add(rewardPackage);
            context_.Add(rewardPackage);
            if (context_.SaveChanges() > 0)
            {
                return rewardPackage;
            }

            return null;
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
                query = query.Where(rp => rp.RewardPackageId == options.RewardPackageId);
            }            

            if (options.Amount != null)
            {
                query = query.Where(rp => rp.Amount == options.Amount);
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

        public RewardPackage UpdateRewardPackage(UpdateRewardPackageOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var rewardPackage = SearchRewardPackage(new SearchRewardPackageOptions()
            {
                RewardPackageId = options.RewardPackageId
            }).SingleOrDefault();

            if (rewardPackage == null)
            {
                return null;
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

            if (options.Rewards == null || !options.Rewards.Any())
            {
                return null;
            }

            foreach (var reward in rewardPackage.Rewards.ToList())
            {
                var deleteReward = rewardService_.RemoveReward(reward.RewardId);

                if (deleteReward == false)
                {
                    return null;
                }
            }

            foreach (var reward in options.Rewards)
            {               
                if (reward == null)
                {
                    return null;
                }

                var createdReward = rewardService_.CreateReward(reward);
            
                rewardPackage.Rewards.Add(createdReward);
            }

            if (context_.SaveChanges() > 0)
            {
                return rewardPackage;
            }

            return null;
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
