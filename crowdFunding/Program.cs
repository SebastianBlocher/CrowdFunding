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

                var user1 = userService.CreateUser(new CreateUserOptions()
                {
                    FirstName = "seb",
                    LastName = "seb",
                    Country = "greece",
                    Email = "seb@mail.com"
                });

                //var user2 = userService.UpdateUser(new UpdateUserOptions()
                //{
                //    UserId = 1,
                //    Country = "trwrter"
                //});

                var project1 = projectService.CreateProject(new CreateProjectOptions()
                {
                    UserId = 1,
                    Name = "sec proj",
                    Description = "sec proj description",
                    Category = (Category)3
                });

                var proj = backedProjectService.SearchBackedProjects(new SearchBackedProjectsOptions()
                {
                    UserId = 2,
                    BackedTo = DateTimeOffset.Now
                }).ToList();

                Console.WriteLine(proj.Count());
            }
        }
    }
}
