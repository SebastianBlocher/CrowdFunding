using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace crowdFunding.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProjectService projectService;
        public IUserService userService;

        public IProjectService ProjectService => projectService;


        public HomeController(ILogger<HomeController> logger, IProjectService project, IUserService user)
        {
            userService = user;
            projectService = project;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var us = userService.CreateUser(new Core.Services.Options.Create.CreateUserOptions()
            {
                FirstName = "M",
                LastName = "L",
                Country = "GR",
                Email = "mema@gmail.com",
                Description = "BLAH BLAH"
            });
            var result = projectService.CreateProject(new Core.Services.Options.Create.CreateProjectOptions()
            {
                UserId = 2,
                AmountRequired = 100M,
                Name = "asd",
                Description = " blah",
                Category = (Category)4

            });

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Project()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
