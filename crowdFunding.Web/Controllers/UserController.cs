using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}