namespace BlogMVC.Domain.Entities;

public sealed class Tags : BaseEntity
{
    public string Tag { get; set; }
    public Post Post { get; set; }
    public int PostId { get; set; }
}
