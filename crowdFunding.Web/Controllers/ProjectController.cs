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

        [HttpPost]
        public IActionResult Create(int userId,
            [FromBody]CreateProjectOptions options)
        {
            var result = projectService.CreateProject(userId, options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data);
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
        public IActionResult Search(SearchProjectOptions options)
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

        [HttpPatch("{id}")]
        public IActionResult Update(int projectId,
            [FromBody]UpdateProjectOptions options)
        {
            var result = projectService.UpdateProject(projectId,
               options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data); ;
        }
        //[HttpDelete("{delete}")]
        //trending
    }
}