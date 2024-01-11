using BlogMVC.Application.Dtos.Tags;

namespace BlogMVC.Application.Dtos.Post;

public class GetPostsResponseJson
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public IEnumerable<GetTagsResponseJson> Tags { get; set; }
    
}
