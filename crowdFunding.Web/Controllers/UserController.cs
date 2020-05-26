using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    public class UserController : Controller
    {
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
            var user = userService.CreateUser(options);

            if (user == null)
            {
                return BadRequest();
            }

            return Json(user);
        }

        [HttpGet]
        public IActionResult GetUser(int? id)
        {
            var user = userService.GetById(id).SingleOrDefault();
            
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost]
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

        [HttpPatch]
        public IActionResult Update([FromBody]UpdateUserOptions options)
        {
            var user = userService.UpdateUser(options);

            if (user == null)
            {
                return BadRequest();
            }

            return Json(user);
        }

        [HttpDelete]
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