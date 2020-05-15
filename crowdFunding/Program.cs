using crowdFunding.Services;
using crowdFunding.Services.Options;
using System;
using System.Linq;

namespace crowdFunding
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrowdFundingDbContext())
            {
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
            }
        }
    }
}
