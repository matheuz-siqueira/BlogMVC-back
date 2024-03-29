namespace BlogMVC.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public IList<Post> Post { get; set; }

}
