using Microsoft.AspNetCore.Mvc;
using Common.Payload.Blog;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var posts = BlogSampleData.Posts;
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            var post = BlogSampleData.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }
    }
}
