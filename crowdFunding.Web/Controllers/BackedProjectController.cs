using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [Route("backedProject")]
    public class BackedProjectController : Controller
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        private IBackedProjectsService backedProjectsService;

        public BackedProjectController(IBackedProjectsService backedProjectsService_)
        {
            backedProjectsService = backedProjectsService_;
        }

        [HttpPost]
        public IActionResult Create(int userId,
            int projectId,
            [FromBody]CreateBackedProjectOptions options)
        {
            var result = backedProjectsService.CreateBackedProject(userId, projectId, options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data);
        }

        [HttpGet]
        public IActionResult Search([FromBody]SearchBackedProjectsOptions options)
        {
            var backedProject = backedProjectsService
                .SearchBackedProjects(options)
                .ToList();

            if (backedProject == null) 
            {
                return BadRequest();
            }

            if(backedProject.Count == 0)
            {
                return NotFound();
            }

            return Json(backedProject);
        }
    }
}