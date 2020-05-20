using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}