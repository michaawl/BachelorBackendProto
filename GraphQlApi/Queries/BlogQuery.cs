using Common.Payload.Blog;

namespace GraphQlApi.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class BlogQuery
    {
        public IEnumerable<BlogPost> GetPosts()
            => BlogSampleData.Posts;
    }
}
