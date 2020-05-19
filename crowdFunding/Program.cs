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

                var user = userService.CreateUser(new CreateUserOptions()
                {
                    FirstName = "Dimitris",
                    LastName = "Pnevmatikos",
                    Country = "greece"
                });
            }
        }
    }
}
