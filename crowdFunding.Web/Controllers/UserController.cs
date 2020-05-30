using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        private IUserService userService;

        public UserController(IUserService userService_)
        {            
            userService = userService_;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateUserOptions options)
        {
            var result = userService.CreateUser(options);

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
            var user = userService.GetById(id).SingleOrDefault();

            return View(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int? id)
        {
            var user = userService.GetById(id).SingleOrDefault();
            
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpGet("search")]
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

        [HttpPatch("{id}")]
        public IActionResult Update(int id,
            [FromBody]UpdateUserOptions options)
        {
            var result = userService.UpdateUser(id,
               options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data); ;
        }

        [HttpDelete("{id}")]
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