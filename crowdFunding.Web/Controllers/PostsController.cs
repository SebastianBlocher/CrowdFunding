using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [Route("post")]
    public class PostController : Controller
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        private IPostService postService;

        public PostController(IPostService postService_)
        {
            postService = postService_;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreatePostOptions options)
        {
            var result = postService.CreatePost(options.ProjectId, options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromForm] int? id)
        {
            var post = postService.SearchPost(new SearchPostOptions() { 
                PostId = id
            });

            if (post == null)
            {
                return NotFound();
            }

            return Json(post);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id,
            [FromBody] UpdatePostOptions options)
        {
            var result = postService.UpdatePost(id,
                options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result.Data); ;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            var isPostRemoved = postService.DeletePost(id);

            if (isPostRemoved == false)
            {
                return BadRequest();
            }

            return Json(isPostRemoved);
        }

        [HttpGet("{id}/search")]
        public IActionResult Search(SearchPostOptions options)
        {
            var posts = postService
                .SearchPost(options)
                .ToList();

            if (!posts.Any())
            {
                return NotFound();
            }

            if (posts == null)
            {
                return BadRequest();
            }

            return Json(posts);
        }
    }
}
