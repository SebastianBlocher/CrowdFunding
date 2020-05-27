using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    public class BackedProjectController : Controller
    {
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
        public IActionResult Create([FromBody]CreateBackedProjectOptions options)
        {
            var backedProject = backedProjectsService.CreateBackedProject(options);

            if (backedProject == null)
            {
                return BadRequest();
            }

            return Json(backedProject);
        }

        [HttpPost]
        public IActionResult Search([FromBody]SearchBackedProjectsOptions options)
        {
            var backedProject = backedProjectsService
                .SearchBackedProjects(options)
                .ToList();

            if (backedProject == null || backedProject.Count == 0)
            {
                return BadRequest();
            }

            return Json(backedProject);
        }
    }
}