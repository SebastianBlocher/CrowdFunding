using crowdFunding.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Services
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
            if (options == null)
            {
                return null;
            }

            var project = projectService_.SearchProject(new SearchProjectOptions()
            {
                projectId = options.ProjectId

            }).SingleOrDefault();

            if (project == null)
            {
                return null;
            }

            var rewardPackage = new RewardPackage()
            {
                Amount = options.Amount,
                Description = options.Description
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
            //----------------------
            var project = projectService_.SearchProject(new SearchProjectOptions()
            {
                projectId = options.ProjectId

            }).SingleOrDefault();

            if (project != null)
            {                
                query = query.Where(rp => rp.projectId == options.ProjectId);
            }
            //-----------------------
            query = query.Take(500);

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

            if (options.RewardIds == null || !options.RewardIds.Any())
            {
                return null;
            }

            rewardPackage.Rewards.Clear();

            foreach (var id in options.RewardIds)
            {
                var reward = rewardService_.GetRewardById(id);

                if (reward == null)
                {
                    return null;
                }

                rewardPackage.Rewards.Add(reward);             
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

            context_.Remove(rewardPackage);

            if (context_.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
