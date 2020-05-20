using crowdFunding.Services;
using crowdFunding.Services.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
//using crowdFunding.Services.Options;

namespace crowdFunding
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrowdFundingDbContext())
            {
                IUserService userService = new UserService(context);
                IProjectService projectService = new ProjectService(context, userService);
                IBackedProjectsService backedProjectService = new BackedProjectsServices(context, userService, projectService);
                IRewardService rewardService = new RewardService(context);
                IRewardPackageService rewardPackageService = new RewardPackageService(context, rewardService, projectService);

                projectService.CreateProject(new CreateProjectOptions());
            }
        }
    }
}
