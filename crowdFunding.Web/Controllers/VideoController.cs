using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [Route("video")]
    public class VideoController : Controller
    {
        private IVideoService videoService;

        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        public VideoController(IVideoService videoService_)
        {
            videoService = videoService_;
        }

        [HttpPost]
        public IActionResult Create(int id, [FromBody] CreateVideoOptions options)
        {
            var result = videoService.CreateVideo(id, options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int? id)
        {
            var video = videoService.SearchVideo(new SearchVideoOptions()
            { 
                VideoId = id
            }).SingleOrDefault();

            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        [HttpGet("search")]
        public IActionResult Search([FromBody] SearchVideoOptions options)
        {
            var video = videoService
                .SearchVideo(options)
                .ToList();

            if (video == null)
            {
                return BadRequest();
            }

            if (video.Count == 0)
            {
                return NotFound();
            }

            return Json(video);
        }        

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int? id)
        {
            var isVideoRemoved = videoService.DeleteVideo(id);

            if (isVideoRemoved == false)
            {
                return BadRequest();
            }

            return Json(isVideoRemoved);
        }
    }
}
