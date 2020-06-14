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

        [HttpPost("create")]
        public IActionResult Create([FromBody]CreateBackedProjectOptions options)
        {
            var result = backedProjectsService.CreateBackedProject(options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data);
        }
    }
}