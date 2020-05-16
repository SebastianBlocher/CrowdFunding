using crowdFunding.Services;
using crowdFunding.Services.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using crowdFunding.Services.Options;

namespace crowdFunding
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrowdFundingDbContext())
            {
<<<<<<< HEAD
                var user = new UserService(context)
                    .CreateUser(new CreateUserOptions()
                    {
                        FirstName = "giannis",
                        LastName = "poulopoulos",
                        Country = "tk"
                    });


                var backedProject = new BackedProjectsServices(context)
                    .SearchBackedProjects(new SearchBackedProjectsOptions()
                    {

                    }, 1);
=======
                
                
                    IProjectService projectservice = new ProjectService(context);
                   
                    var proj = projectservice.CreateProject(new crowdFunding.Services.Options.CreateProjectOption()
                    { 
                        Name = "Project 10",
                        Description = "Testtest",
                        Category = (Category)3
                    }
                    );

                var project = projectservice.SearchProject( new SearchProjectOption
                {
                    ProjectId=1,
                }).FirstOrDefault();
>>>>>>> master_project_services
            }
        }
    }
}
