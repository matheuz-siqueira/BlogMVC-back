using BlogMVC.Domain.Validation;

namespace BlogMVC.Domain.Entities;

public sealed class Post : BaseEntity
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdateAt { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    
}
