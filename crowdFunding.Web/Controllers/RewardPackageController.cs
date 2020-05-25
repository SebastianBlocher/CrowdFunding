using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [Route("rewardPackage")]
    public class RewardPackageController : Controller
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        private IRewardPackageService rewardPackageService;

        public RewardPackageController(IRewardPackageService rewardPackageService_)
        {
            rewardPackageService = rewardPackageService_;
        }

        [HttpPost]
        public IActionResult Create(int projectId,
            [FromBody]CreateRewardPackageOptions options)
        {
            var result = rewardPackageService.CreateRewardPackage(projectId, options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int? rewardPackageId)
        {
            var rewardPackage = rewardPackageService.GetRewardPackageById(rewardPackageId);

            if (rewardPackage == null)
            {
                return NotFound();
            }

            return Json(rewardPackage);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id,
            [FromBody]UpdateRewardPackageOptions options)
        {
            var result = rewardPackageService.UpdateRewardPackage(id,
                options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data); ;
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromForm]int? rewardPackageId)
        {
            var isRewardPackageRemoved = rewardPackageService.RemoveRewardPackage(rewardPackageId);

            if (isRewardPackageRemoved == false)
            {
                return BadRequest();
            }

            return Json(isRewardPackageRemoved);
        }

        [HttpGet("search")]
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