using BlogMVC.Domain.Validation;

namespace BlogMVC.Domain.Entities;

public sealed class Comment : BaseEntity
{

    public Comment(string commentary, int postId)
    {
        ValidationDomain(commentary, postId); 
    }
    public Comment(int id, string commentary, int postId)
    {
        DomainExceptionValidation.When(id < 0, "Id inválido");
        Id = id; 
        ValidationDomain(commentary, postId); 
    }

    public string Commentary { get; private set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Post Post { get; set; }
    public int PostId { get; private set; }   

    private void ValidationDomain(string commentary, int postId)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(commentary) && commentary.Length < 3,
            "Comentário precisa ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(string.IsNullOrEmpty(commentary) && commentary.Length > 100,
            "Comentário precisa ter no máximo 100 caracteres"); 
        
        DomainExceptionValidation.When(postId < 0, "Id inválido"); 
    
        Commentary = commentary; 
        PostId = postId; 
    }

    public void Update(string commentary, int postId)
    {
        ValidationDomain(commentary, postId); 
    }
}
