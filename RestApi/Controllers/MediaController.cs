using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Common;

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

        private FileStreamResult LoadMedia(string resourceName, string contentType)
        {
            var assembly = typeof(TextPayload).Assembly;
            var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                var resources = string.Join("\n", assembly.GetManifestResourceNames());
                throw new FileNotFoundException($"Resource '{resourceName}' not found. Available:\n{resources}");
            }

            return File(stream, contentType);
        }

        [HttpGet("image")]
        public IActionResult GetImage()
        {
            return LoadMedia("Common.Payload.Media.foto.jpg", "image/jpeg");
        }

        [HttpGet("audio")]
        public IActionResult GetAudio()
        {
            return LoadMedia("Common.Payload.Media.music.wav", "audio/wav");
        }

        [HttpGet("video")]
        public IActionResult GetVideo()
        {
            return LoadMedia("Common.Payload.Media.video.mp4", "video/mp4");
        }
    }
}
