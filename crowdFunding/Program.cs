using crowdFunding.Services;
using crowdFunding.Services.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
//using crowdFunding.Services.Options;
using crowdFunding.Options;

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


                //var search = projectService.SearchProject(new SearchProjectOptions()
                //{
                //    ProjectId = 1

                //}).SingleOrDefault();
                //var proj = projectService.CreateProject(new CreateProjectOptions()
                //{
                //    Name = "proj 3",
                //    Description = "None",
                //    UserId = 1,
                //    Category = (Category).2
                //});

                //var reward1 = new CreateRewardOptions()
                //{
                //    Name = "tttt"
                //};

                //var reward2 = new CreateRewardOptions()
                //{
                //    Name = "gggggg"
                //};

                //var rewards = new List<CreateRewardOptions>();
                //rewards.Add(reward1);
                //rewards.Add(reward2);
                //var packlage = rewardPackageService.CreateRewardPackage(new CreateRewardPackageOptions()
                //{
                //    Name = "Package 2",
                //    Description = "Wow",
                //    Amount = 345345M,
                //    ProjectId = 3,
                //    RewardOptions = rewards
                //});
                //var user1 = backedProjectService.CreateBackedProject(new CreateBackedProjectOptions()
                //{
                //   ProjectId = 1,
                //   Amount = 515M,
                //    UserId = 1
                //});

                //var user2 = backedProjectService.CreateBackedProject(new CreateBackedProjectOptions()
                //{
                //    ProjectId = 2,
                //    Amount = 5665M,
                //    UserId =2
                //});

                //var user3 = backedProjectService.CreateBackedProject(new CreateBackedProjectOptions()
                //{
                //    ProjectId = 2,
                //    Amount = 345M,
                //    UserId = 1
                //});
                var trending = projectService.TrendingProjects();
                foreach (var i in trending)
                {
                    Console.WriteLine(i);
                }
                ////var user2 = userService.UpdateUser(new UpdateUserOptions()
                ////{
                ////    UserId = 1,
                ////    Country = "trwrter"
                ////});

                //var project1 = projectService.CreateProject(new CreateProjectOptions()
                //{
                //    UserId = 1,
                //    Name = "sec proj",
                //    Description = "sec proj description",
                //    Category = (Category)3
                //});

                //var proj = backedProjectService.SearchBackedProjects(new SearchBackedProjectsOptions()
                //{
                //    UserId = 2,
                //    BackedTo = DateTimeOffset.Now
                //}).ToList();            
            }
        }
    }
}
