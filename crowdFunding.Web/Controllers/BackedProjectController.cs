using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crowdFunding.Core.Data;
using crowdFunding.Core.Services;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using Microsoft.AspNetCore.Mvc;

namespace crowdFunding.Web.Controllers
{
    public class BackedProjectController : Controller
    {
        [Route("{backedproject}")]
        public IActionResult Index()
        {
            return View();
        }

        private CrowdFundingDbContext context;
        private IBackedProjectsService backedProjectsService;
        private IUserService userService;
        private IProjectService projectService;

        public BackedProjectController()
        {
            context = new CrowdFundingDbContext();
            backedProjectsService = new BackedProjectsServices(context, userService, projectService);
        }

        [HttpPost("{create}")]
        public IActionResult Create([FromBody]CreateBackedProjectOptions options)
        {
            var backedProject = backedProjectsService.CreateBackedProject(options);

            if (backedProject == null)
            {
                return BadRequest();
            }

            return Json(backedProject);
        }

        [HttpPost("{search}")]
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