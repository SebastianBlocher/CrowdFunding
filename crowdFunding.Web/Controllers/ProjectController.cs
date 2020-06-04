using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [Route("project")]
    public class ProjectController : Controller
    {
        private IProjectService projectService;

        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        public ProjectController(IProjectService projectService_)
        {
            projectService = projectService_;
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

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet("edit")]
        public IActionResult Edit(int? id)
        {
            var project = projectService.GetProjectById(id).SingleOrDefault();

            return View(project);
        }

        [HttpGet("{id}")]
        public IActionResult GetProject(int? id)
        {
            var project = projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Json(project);
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

            if (project.Count == 0)
            {
                return NotFound();
            }

            return Json(project);
        }

        [HttpPatch("{id}")]
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

            return Json(result.Data);
        }

        [HttpDelete("{id}")]
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