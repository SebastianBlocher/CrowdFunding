﻿using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using crowdFunding.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Diagnostics;
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
            var proj = projectService.GetProjectById(id);

            if (proj == null) return View(null);

            var viewModel = new ProjectViewModel()
            {
                Project = proj,

                RewardPackages = proj.RewardPackages,

                User = proj.User,
            };
            
            foreach (var rw in proj.RewardPackages)
            {
                viewModel.Rewards = rw.Rewards;
            }

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

                User = context.Set<User>().SingleOrDefault()
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