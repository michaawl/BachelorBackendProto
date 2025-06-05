using Common.Payload.Blog;

namespace GraphQlApi.Queries;

public class BlogQuery
{
    public IEnumerable<BlogPost> GetPosts() => BlogSampleData.Posts;

    public BlogPost? GetPostById(int id) =>
        BlogSampleData.Posts.FirstOrDefault(p => p.Id == id);
}

