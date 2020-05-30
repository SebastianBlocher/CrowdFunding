using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace crowdFunding.Web.Controllers
{
    [Route("photo")]
    public class PhotoController : Controller
    {
        private IPhotoService photoService;

        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        public PhotoController(IPhotoService photoService_)
        {
            photoService = photoService_;
        }

        [HttpPost]
        public IActionResult Create(int id, [FromBody] CreatePhotoOptions options)
        {
            var result = photoService.CreatePhoto(id, options);

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
            var photo = photoService.SearchPhoto(new SearchPhotoOptions()
            {
                PhotoId = id
            }).SingleOrDefault();

            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        [HttpGet("search")]
        public IActionResult Search([FromBody] SearchPhotoOptions options)
        {
            var photo = photoService
                .SearchPhoto(options)
                .ToList();

            if (photo == null)
            {
                return BadRequest();
            }

            if (photo.Count == 0)
            {
                return NotFound();
            }

            return Json(photo);
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int? id)
        {
            var isPhotoRemoved = photoService.DeletePhoto(id);

            if (isPhotoRemoved == false)
            {
                return BadRequest();
            }

            return Json(isPhotoRemoved);
        }
    }
}
