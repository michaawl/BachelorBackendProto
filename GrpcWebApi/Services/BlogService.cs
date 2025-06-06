using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcWebApi.Protos;

namespace GrpcWebApi.Services
{
    public class BlogService : Blog.BlogBase
    {
        private readonly ILogger<BlogService> _logger;

        public BlogService(ILogger<BlogService> logger)
        {
            _logger = logger;
        }

        public override Task<BlogPostsResponse> GetAll(Empty request, ServerCallContext context)
        {
            var posts = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = 1,
                    Title = "Hello from gRPC!",
                    Author = new Author { Name = "Alice", Email = "alice@example.com" },
                    Sections = { new BlogSection { Heading = "Intro", Body = "Welcome to gRPC-Web!" } },
                    Media = new MediaBlock { ImageUrl = "/images/1.jpg" },
                    Metadata = new GrpcWebApi.Protos.Metadata { Tags = { "gRPC", "Demo" }, WordCount = 42 },
                    PublishedAt = "2024-06-07T10:00:00Z"
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "Second Post",
                    Author = new Author { Name = "Bob", Email = "bob@example.com" },
                    Sections = { new BlogSection { Heading = "Main", Body = "Second body." } },
                    Media = new MediaBlock { VideoUrl = "/videos/2.mp4" },
                    Metadata = new GrpcWebApi.Protos.Metadata { Tags = { "Backend" }, WordCount = 21 },
                    PublishedAt = "2024-06-06T15:30:00Z"
                }
            };

            var response = new BlogPostsResponse();
            response.Posts.AddRange(posts);
            return Task.FromResult(response);
        }
    }
}
