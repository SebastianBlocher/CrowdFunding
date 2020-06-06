using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using crowdFunding.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [Route("project")]
    public class ProjectController : Controller
    {
        private IProjectService projectService;
        private IRewardService rewardService;
        public IRewardPackageService rewardPackageService;
        public IUserService userService;

        public ProjectController(IProjectService projectService_,
            IRewardService rewardService_,
            IRewardPackageService rewardPackageService_,
            IUserService userService_)
        {
            projectService = projectService_;
            rewardPackageService = rewardPackageService_;
            rewardService = rewardService_;
            userService = userService_;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("edit")]
        public IActionResult Edit(int? id)
        {
            var project = projectService.GetProjectById(id);

            return View(project);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]CreateProjectOptions options)
        {
            var result = projectService.CreateProject(options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int? id)
        {
            var viewModel = new ProjectViewModel()
            {
                Project = projectService.GetProjectById(id),

                //    RewardPackages = rewardPackageService
                //    .SearchRewardPackage(new SearchRewardPackageOptions()).ToList(),

                //    Rewards = rewardService
                //    .SearchReward(new SearchRewardOptions()).ToList(),

                //    User = userService.SearchUsers(new SearchUserOptions()).SingleOrDefault()

            };
            //var proj = projectService.GetProjectById(id);
            //if (proj == null)
            //{
            //    return View(null);
            //}

            //var viewModel = new ProjectViewModel()
            //{
            //    Project = proj,
            //    RewardPackages = proj.RewardPackages,
            //    User = proj.User
            //};

            //foreach (var rw in proj.RewardPackages)
            //{
            //    viewModel.Rewards = rw.Rewards;
            //}

            return View(viewModel);           
        }

        [HttpGet("search")]
        public IActionResult Search(SearchProjectOptions options)
        {
            var viewModel = new ProjectSearchViewModel()
            {
                ProjectList = projectService
                .SearchProject(options)
                .ToList(),
                
                //Users = userService.SearchUsers(new SearchUserOptions()).ToList()
            };

            return View(viewModel);
        }

        [HttpPatch("{id}/edit")]
        public IActionResult Update(int id,
            [FromBody]UpdateProjectOptions options)
        {
            var result = projectService.UpdateProject(id,
               options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data); ;
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Remove(int? id)
        {
            var isProjectRemoved = projectService.DeleteProject(id);

            if (isProjectRemoved == false)
            {
                return BadRequest();
            }

            return Json(isProjectRemoved);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}