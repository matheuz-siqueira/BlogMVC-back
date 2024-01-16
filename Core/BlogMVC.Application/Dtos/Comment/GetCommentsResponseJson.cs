namespace BlogMVC.Application.Dtos.Comment;

public class GetCommentsResponseJson
{
    public int Id { get; set; }
    public string Commentary { get; set; }
    public int PostId { get; set; }
}
