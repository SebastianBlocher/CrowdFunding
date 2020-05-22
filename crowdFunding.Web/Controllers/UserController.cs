using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crowdFunding.Core.Data;
using crowdFunding.Core.Services;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.AspNetCore.Mvc;

namespace crowdFunding.Web.Controllers
{
    public class UserController : Controller
    {
        [Route("{user}")]
        public IActionResult Index()
        {
            return View();
        }

        private CrowdFundingDbContext context;
        private IUserService userService;

        public UserController()
        {
            context = new CrowdFundingDbContext();
            userService = new UserService(context);
        }

        [HttpPost("{create}")]
        public IActionResult Create([FromBody]CreateUserOptions options)
        {
            var user = userService.CreateUser(options);

            if (user == null)
            {
                return BadRequest();
            }

            return Json(user);
        }

        [HttpGet("{getuser}")]
        public IActionResult GetUser(int? id)
        {
            var user = userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost("{search}")]
        public IActionResult Search([FromBody]SearchUserOptions options)
        {
            var users = userService
                .SearchUsers(options)
                .ToList();

            if (users == null)
            {
                return BadRequest();
            }

            if (users.Count == 0)
            {
                return NotFound();
            }

            return Json(users);
        }

        [HttpPatch("{update}")]
        public IActionResult Update([FromBody]UpdateUserOptions options)
        {
            var user = userService.UpdateUser(options);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{delete}")]
        public IActionResult Disable(int? id)
        {
            if (userService.DisableUser(id))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}