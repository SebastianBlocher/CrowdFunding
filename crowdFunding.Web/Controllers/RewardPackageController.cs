using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RewardPackageController : Controller
    {
        [Route("[controller]")]
        [HttpGet("{Index}")]
        public IActionResult Index()
        {
            return View();
        }

        private IRewardService rewardService;
        private IProjectService projectService;
        private IRewardPackageService rewardPackageService;

        public RewardPackageController(IRewardPackageService rewardPackageService_, IRewardService rewardService_, IProjectService projectService_)
        {
            projectService = projectService_;
            rewardService = rewardService_;
            rewardPackageService = rewardPackageService_;
        }

        [HttpPost("{Create}")]
        public IActionResult Create([FromBody]CreateRewardPackageOptions options)
        {
            var rewardPackage = rewardPackageService.CreateRewardPackage(options);

            if (rewardPackage == null)
            {
                return BadRequest();
            }

            return Json(rewardPackage);
        }

        [HttpGet("{GetById}")]
        public IActionResult GetById([FromBody]int? rewardPackageId)
        {
            var rewardPackage = rewardPackageService.GetRewardPackageById(rewardPackageId);

            if (rewardPackage == null)
            {
                return NotFound();
            }

            return Json(rewardPackage);
        }

        [HttpPatch("{Update}")]
        public IActionResult Update([FromBody]UpdateRewardPackageOptions options)
        {
            var rewardPackage = rewardPackageService.UpdateRewardPackage(options);

            if (rewardPackage == null)
            {
                return BadRequest();
            }

            return Json(rewardPackage);
        }

        [HttpDelete("{Remove}")]
        public IActionResult Remove([FromBody]int? rewardPackageId)
        {
            var isRewardPackageRemoved = rewardPackageService.RemoveRewardPackage(rewardPackageId);

            if (isRewardPackageRemoved == false)
            {
                return BadRequest();
            }

            return Json(isRewardPackageRemoved);
        }

        [HttpGet("{Search}")]
        public IActionResult Search([FromBody]SearchRewardPackageOptions options)
        {
            var rewardPackages = rewardPackageService
                .SearchRewardPackage(options)
                .ToList();

            if (!rewardPackages.Any())
            {
                return NotFound();
            }

            if (rewardPackages == null)
            {
                return BadRequest();
            }

            return Json(rewardPackages);
        }
    }
}