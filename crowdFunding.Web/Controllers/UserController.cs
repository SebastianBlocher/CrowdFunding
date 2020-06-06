using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using crowdFunding.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost("create")]
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

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int? id)
        {
            var user = userService.GetById(id).SingleOrDefault();

            return View(user);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int? id)
        {
            var viewModel = new UserViewModel()
            {
                User = userService.GetById(id)
                .Include(u => u.BackedProjectsList)                
                .Include(u => u.CreatedProjectsList)
                .ThenInclude(u => u.Photos) 
                .SingleOrDefault()
            };

            return View(viewModel);
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

        [HttpPatch("update")]
        public IActionResult Update(
            [FromBody]UpdateUserOptions options)
        {
            var result = userService.UpdateUser(options.UserId,
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

        [HttpPost("login")]
        public IActionResult Login([FromBody]SearchUserOptions options)
        {
            var user = userService
                .SearchUsers(options)
                .SingleOrDefault();

            if (user == null)
            {
                return BadRequest();
            }

            return Json(user);
        }
    }
}