// GraphQlApi/Queries/BlogQuery.cs
using Common.Payload.Blog;
using HotChocolate;
using HotChocolate.Types;   // <-- for [ExtendObjectType]

namespace GraphQlApi.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class BlogQuery
    {
        public IEnumerable<BlogPost> GetPosts()
            => BlogSampleData.Posts;

        public BlogPost? GetPostById(int id)
            => BlogSampleData.Posts.FirstOrDefault(p => p.Id == id);
    }
}
