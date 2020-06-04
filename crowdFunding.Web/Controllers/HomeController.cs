using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProjectService projectService;
        public IUserService userService;

        public HomeController(ILogger<HomeController> logger, IProjectService iproj, CrowdFundingDbContext dbContext)
        {
            userService = user;
            projectService = project;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var tpl = new List<Project>();
            var trendingProjectList = projectService
                     .TrendingProjects();

            foreach (var i in trendingProjectList)
            {
                tpl.Add(projectService.GetProjectById(i));
            }

            return View(tpl);
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