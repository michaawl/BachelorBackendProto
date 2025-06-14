using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcWebApi.Protos;
using Common.Payload.Blog;

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
            var response = new BlogPostsResponse();

            foreach (var post in BlogSampleData.Posts)
            {
                var grpcPost = new GrpcWebApi.Protos.BlogPost
                {
                    Id = post.Id,
                    Title = post.Title,
                    Author = new GrpcWebApi.Protos.Author
                    {
                        Name = post.Author.Name,
                        Email = post.Author.Email
                    },
                    Metadata = new GrpcWebApi.Protos.Metadata
                    {
                        WordCount = post.Metadata.WordCount
                    },
                    Numbers = new GrpcWebApi.Protos.NumbersBlock
                    {
                        NumberOne = post.Numbers.NumberOne,
                        NumberTwo = post.Numbers.NumberTwo,
                        NumberThree = post.Numbers.NumberThree,
                        NumberFour = post.Numbers.NumberFour
                    },
                    PublishedAt = Timestamp.FromDateTime(post.PublishedAt.ToUniversalTime())
                };

                grpcPost.Metadata.Tags.AddRange(post.Metadata.Tags);

                foreach (var section in post.Sections)
                {
                    grpcPost.Sections.Add(new GrpcWebApi.Protos.BlogSection
                    {
                        Heading = section.Heading,
                        Body = section.Body
                    });
                }

                response.Posts.Add(grpcPost);
            }

            return Task.FromResult(response);
        }
    }
}
