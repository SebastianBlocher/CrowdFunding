using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [Route("reward")]
    public class RewardController : Controller
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
        
        private IRewardService rewardService;

        public RewardController(IRewardService rewardService_)
        {
            rewardService = rewardService_;
        }

        [HttpPost]
        public IActionResult Create(int projectId, [FromBody]CreateRewardOptions options)
        {
            var reward = rewardService.CreateReward(projectId, options);

            if (reward == null)
            {
                return BadRequest();
            }

            return Json(reward);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromForm]int? rewardId)
        {
            var reward = rewardService.GetRewardById(rewardId);

            if (reward == null)
            {
                return NotFound();
            }

            return Json(reward);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int rewardId,
            [FromBody]UpdateRewardOptions options)
        {
            var result = rewardService.UpdateReward(rewardId,
                options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data); ;
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromForm]int? rewardId)
        {
            var isRewardRemoved = rewardService.RemoveReward(rewardId);

            if (isRewardRemoved == false)
            {
                return BadRequest();
            }

            return Json(isRewardRemoved);
        }

        [HttpGet("{id}/search")]
        public IActionResult Search([FromBody]SearchRewardOptions options)
        {
            var rewards = rewardService
                .SearchReward(options)
                .ToList();

            if (!rewards.Any())
            {
                return NotFound();
            }

            if (rewards == null)
            {
                return BadRequest();
            }

            return Json(rewards);
        }
    }
}