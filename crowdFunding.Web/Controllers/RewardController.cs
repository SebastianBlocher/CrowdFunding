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
    public class RewardController : Controller
    {
        
        [Route("{reward}")]
        [HttpGet("{Index}")]
        public IActionResult Index()
        {
            return View();
        }
        
        private IRewardService rewardService;

        public RewardController(IRewardService rewardService_)
        {
            rewardService = rewardService_;
        }

        [HttpPost("{Create}")]
        public IActionResult Create([FromBody]CreateRewardOptions options)
        {
            var reward = rewardService.CreateReward(options);

            if (reward == null)
            {
                return BadRequest();
            }

            return Json(reward);
        }

        [HttpGet("{GetById}")]
        public IActionResult GetById([FromBody]int? rewardId)
        {
            var reward = rewardService.GetRewardById(rewardId);

            if (reward == null)
            {
                return NotFound();
            }

            return Json(reward);
        }

        [HttpPatch("{Update}")]
        public IActionResult Update([FromBody]UpdateRewardOptions options)
        {
            var reward = rewardService.UpdateReward(options);

            if (reward == null)
            {
                return BadRequest();
            }

            return Json(reward);
        }

        [HttpDelete("{Remove}")]
        public IActionResult Remove([FromBody]int? rewardId)
        {
            var isRewardRemoved = rewardService.RemoveReward(rewardId);

            if (isRewardRemoved == false)
            {
                return BadRequest();
            }

            return Json(isRewardRemoved);
        }

        [HttpGet("{Search}")]
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