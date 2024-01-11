using BlogMVC.Application.Dtos.Comment;
using BlogMVC.Application.Dtos.Tags;

namespace BlogMVC.Application.Dtos.Post;

public class CreatePostRequestJson 
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Content { get; set; }
    public IEnumerable<CreateTagRequestJson> Tags { get; set; }
}
