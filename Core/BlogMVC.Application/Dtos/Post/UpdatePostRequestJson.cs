using BlogMVC.Application.Dtos.Tags;

namespace BlogMVC.Application.Dtos.Post;

public class UpdatePostRequestJson
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Content { get; set; }
    public IList<CreateTagRequestJson> Tags { get; set; }
}
