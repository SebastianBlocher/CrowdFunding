using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectService projectService;

        public IActionResult Index()
        {
            return View();
        }

        public ProjectController(IProjectService projectService_)
        {
            projectService = projectService_;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateProjectOptions options)
        {
            var project = projectService.CreateProject(options);

            if (project == null)
            {
                return BadRequest();
            }
            return Json(project);
        }

        [HttpGet]
        public IActionResult GetProject(int? id)
        {
            var project = projectService.GetProjectById(id);
            if (project == null)
            {
                return BadRequest();
            }
            return Json(project);
        }

        [HttpPost]
        public IActionResult Search(SearchProjectOptions options)
        {
            var project = projectService
                .SearchProject(options)
                .ToList();
            if (project == null)
            {
                return BadRequest();
            }
            return Json(project);
        }

        [HttpPatch]
        public IActionResult Update([FromBody]UpdateProjectOptions options)
        {
            var project = projectService.UpdateProject(options);
            if (project == null)
            { 
                return BadRequest();
            }
            return Ok();
        }
        //[HttpDelete("{delete}")]
        //trending
    }
}