using Microsoft.AspNetCore.Mvc;
using Common;
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

        [HttpGet("small")]
        public ActionResult<TextPayload> GetSmall()
        {
            string content = ApiCache.SmallText;
            return Ok(new TextPayload { Content = content });
        }

        [HttpGet("medium")]
        public ActionResult<TextPayload> GetMedium()
        {
            string content = ApiCache.MediumText;
            return Ok(new TextPayload { Content = content });
        }

        [HttpGet("large")]
        public ActionResult<TextPayload> GetLarge()
        {
            string content = ApiCache.LargeText;
            return Ok(new TextPayload { Content = content });
        }
    }
}
