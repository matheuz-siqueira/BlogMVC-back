using BlogMVC.Domain.Validation;

namespace BlogMVC.Domain.Entities;

public sealed class Comment : BaseEntity
{

    public Comment(string commentary)
    {
        ValidationDomain(commentary); 
    }
    public Comment(int id, string commentary)
    {
        DomainExceptionValidation.When(id < 0, "Id inválido");
        Id = id; 
        ValidationDomain(commentary); 
    }

    public string Commentary { get; private set; }
    public Post Post { get; set; }
    public int PostId { get; private set; }   

    private void ValidationDomain(string commentary)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(commentary) && commentary.Length < 3,
            "Comentário precisa ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(string.IsNullOrEmpty(commentary) && commentary.Length > 100,
            "Comentário precisa ter no máximo 100 caracteres"); 
    
        Commentary = commentary; 
    }

    public void Update(string commentary)
    {
        ValidationDomain(commentary); 
    }
}
