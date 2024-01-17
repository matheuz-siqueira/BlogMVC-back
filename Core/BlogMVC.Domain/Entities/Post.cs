using BlogMVC.Domain.Validation;

namespace BlogMVC.Domain.Entities;

public sealed class Post : BaseEntity
{
    public Post(string title, string subtitle, string content)
    {
        ValidationDomain(title, subtitle, content);        
    }
    public Post(int id, string title, string subtitle, string content)
    {
        DomainExceptionValidation.When(id < 0, "Id inválido"); 
        Id = id; 
        ValidationDomain(title, subtitle, content); 
    }

    public string Title { get; private set; }
    public string Subtitle { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; } 
    public IList<Tags> Tags { get; private set; }
    public ICollection<Comment> Comments { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    private void ValidationDomain(string title, string subtitle, string content)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(title), "Título é requirido");
        DomainExceptionValidation.When(title.Length < 5, "Título precisa ter no mínimo 5 caracteres"); 
        DomainExceptionValidation.When(title.Length > 120, "Título precisa ter no máximo 120 caracteres");

        DomainExceptionValidation.When(!string.IsNullOrEmpty(subtitle) && subtitle.Length < 3
            , "Subtítulo precisa ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(subtitle.Length > 120, "Subtítulo precisa ter no máximo 120 caracteres");
    
        DomainExceptionValidation.When(string.IsNullOrEmpty(content), "Contéudo não pode ser vazio"); 
        DomainExceptionValidation.When(content.Length > 2000, "O conteúdo precisa ter no máximo 2000 caracteres"); 

        Title = title; 
        Subtitle = subtitle; 
        Content = content;  
    }

    public void AddDate()
    {
        CreatedAt = DateTime.Now; 
    }
    public void Update(string title, string subtitle, string content, IList<Tags> tags)
    {
        ValidationDomain(title, subtitle, content);
        
        if(tags.Any())
        {
            foreach(var tag in tags)
            {
                Tags.Add(tag); 
            }
        }
        else
            Tags.Clear(); 
    }
}
