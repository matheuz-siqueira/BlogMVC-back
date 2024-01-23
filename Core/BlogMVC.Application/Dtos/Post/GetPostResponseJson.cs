using BlogMVC.Application.Dtos.Comment;

namespace BlogMVC.Application.Dtos.Post;

public class GetPostResponseJson
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Content { get; set; } 
    public DateTime CreatedAt { get; set; }
    public IEnumerable<GetCommentsResponseJson> Comments { get; set; }
}
