using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crowdFunding.Core.Data;
using crowdFunding.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options;
using crowdFunding.Core.Services;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;

namespace crowdFunding.Web.Controllers
{

    public class ProjectController : Controller
    {
        private CrowdFundingDbContext context;
        private IProjectService projectService;
        private IUserService userService;

        [Route("{project}")]
        public IActionResult Index()
        {
            return View();
        }

        public ProjectController()
        {
            context = new CrowdFundingDbContext();
            projectService = new ProjectService(context, userService);
        }

        [HttpPost("{create}")]
        public IActionResult Create(CreateProjectOptions options)
        {
            var project = projectService.CreateProject(options);

            if (project == null)
            {
                return BadRequest();
            }
            return Json(project);
        }
        [HttpGet("{getproject}")]
        public IActionResult GetProject(int? id)
        {
            var project = projectService.GetProjectById(id);
            if (project == null)
            {
                return BadRequest();
            }
            return Json(project);
        }

        [HttpPost("{search}")]
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
        [HttpPatch("{update}")]
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