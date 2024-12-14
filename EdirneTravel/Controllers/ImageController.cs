using EdirneTravel.Application.Services.ImageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdirneTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductImage(int id)
        {
            var result =  _imageService.GetImage(id);

            if (result.Success)
            {
                var file = File(result.Data, "image/jpeg", "travel.jpeg");
                return file;
            }

            return BadRequest(result);
        }
    }
}
