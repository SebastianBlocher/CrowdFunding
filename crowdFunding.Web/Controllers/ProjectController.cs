using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using crowdFunding.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [Route("project")]
    public class ProjectController : Controller
    {
        private IProjectService projectService;
        private CrowdFundingDbContext context;        

        public ProjectController(IProjectService projectService_, CrowdFundingDbContext context_)
        {
            projectService = projectService_;
            context = context_;            
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
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

                RewardPackages = context.Set<RewardPackage>()
                .ToList(),

                Rewards = context.Set <Reward>()                
                .ToList(),
            };

            return View(viewModel);
        }

        [HttpGet("search")]
        public IActionResult Search([FromBody]SearchProjectOptions options)
        {
            var project = projectService
                .SearchProject(options)
                .ToList();

            if (project == null)
            {
                return BadRequest();
            }

            if(project.Count == 0)
            {
                return NotFound();
            }

            return Json(project);
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

    }
}