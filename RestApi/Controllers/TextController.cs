using Microsoft.AspNetCore.Mvc;
using Common;
using System.IO;
using System.Reflection;
using System.Text;

//MAYBE SHOULD BE LOADED IN FIRST and not loaded while request?

namespace RestApi.Controllers
{
    [ApiController]
    [Route("text")]
    public class TextController : ControllerBase
    {
        private readonly ILogger<TextController> _logger;

        public TextController(ILogger<TextController> logger)
        {
            _logger = logger;
        }

        private string LoadEmbeddedText(string resourceName)
        {
            var assembly = typeof(TextPayload).Assembly;
            var allResources = assembly.GetManifestResourceNames();
            _logger.LogInformation("Available resources:\n" + string.Join("\n", allResources));

            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new Exception($"Resource '{resourceName}' not found. Available:\n" + string.Join("\n", allResources));
            }

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }


        [HttpGet("small")]
        public ActionResult<TextPayload> GetSmall()
        {
            string content = LoadEmbeddedText("Common.Payload.Text.small.txt");

            int byteSize = Encoding.UTF8.GetByteCount(content);
            _logger.LogInformation($"[SMALL] Payload size: {byteSize} bytes");

            return Ok(new TextPayload { Content = content });
        }

        [HttpGet("medium")]
        public ActionResult<TextPayload> GetMedium()
        {
            string content = LoadEmbeddedText("Common.Payload.Text.medium.txt");

            int byteSize = Encoding.UTF8.GetByteCount(content);
            _logger.LogInformation($"[MEDIUM] Payload size: {byteSize} bytes");

            return Ok(new TextPayload { Content = content });
        }

        [HttpGet("large")]
        public ActionResult<TextPayload> GetLarge()
        {
            string content = LoadEmbeddedText("Common.Payload.Text.large.txt");

            int byteSize = Encoding.UTF8.GetByteCount(content);
            _logger.LogInformation($"[LARGE] Payload size: {byteSize} bytes");

            return Ok(new TextPayload { Content = content });
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");
    }
}
