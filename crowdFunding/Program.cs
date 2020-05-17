using crowdFunding.Services;
using crowdFunding.Services.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
//using crowdFunding.Services.Options;

namespace crowdFunding
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrowdFundingDbContext())
            {
                IUserService userservice = new UserService(context);
                IBackedProjectsService backedProjectService = new BackedProjectsServices(context);
                IProjectService projectservice = new ProjectService(context);

                //var user = userservice
                //    .CreateUser(new CreateUserOptions()
                //    {
                //        FirstName = "giannis",
                //        LastName = "poulopoulos",
                //        Country = "tk"
                //    });


                //var backedProject = backedProjectService
                //    .CreateBackedProject(new CreateBackedProjectOptions()
                //    {
                //        ProjectId = 6,
                //        Amount = 15m,
                //        UserId = 2
                //    });

                var backedProjectList = backedProjectService
                    .SearchBackedProjects(new SearchBackedProjectsOptions()
                    {
                        UserId = 2,
                        ProjectId = 6,
                    }).ToList();

                var user = userservice.GetById(2).ToList();

                Console.WriteLine(user[0].BackedProjectsList.Count());

                //var proj = projectservice.CreateProject(new crowdFunding.Services.Options.CreateProjectOption()
                //{
                //    Name = "Project 10",
                //    Description = "Testtest",
                //    Category = (Category)3
                //}
                //);

                //var project = projectservice.SearchProject(new SearchProjectOption
                //{
                //    ProjectId = 1,
                //}).FirstOrDefault();
            }
        }
    }
}
