using BlogMVC.Domain.Validation;

namespace BlogMVC.Domain.Entities;

public sealed class Comment : BaseEntity
{
    public string Commentary { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Post Post { get; set; }
    public int PostId { get; set; }   
}
