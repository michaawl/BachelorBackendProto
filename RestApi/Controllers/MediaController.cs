using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("media")]
    public class MediaController : ControllerBase
    {
        private readonly ILogger<MediaController> _logger;

        public MediaController(ILogger<MediaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("image")]
        public IActionResult GetImage()
        {
            if (ApiCache.ImageData == null || ApiCache.ImageData.Length == 0)
            {
                return NotFound("Image resource not found in MediaCache.");
            }

            return File(ApiCache.ImageData, "image/jpeg");
        }

        [HttpGet("audio")]
        public IActionResult GetAudio()
        {
            if (ApiCache.AudioData == null || ApiCache.AudioData.Length == 0)
            {
                return NotFound("Audio resource not found in MediaCache.");
            }

            return File(ApiCache.AudioData, "audio/wav");
        }

        [HttpGet("video")]
        public IActionResult GetVideo()
        {
            if (ApiCache.VideoData == null || ApiCache.VideoData.Length == 0)
            {
                return NotFound("Video resource not found in MediaCache.");
            }

            return File(ApiCache.VideoData, "video/mp4");
        }
    }
}
