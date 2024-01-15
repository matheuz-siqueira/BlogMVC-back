using BlogMVC.Application.Dtos.Post;
using FluentValidation;

namespace BlogMVC.Application.Validators;

public class UpdatePostValidator : AbstractValidator<UpdatePostRequestJson>
{
    public UpdatePostValidator()
    {
        RuleFor(r => r.Title).NotEmpty().WithMessage("Título é requirido")
            .MinimumLength(5).WithMessage("Título precisa ter no mínimo 5 caracteres")
            .MaximumLength(120).WithMessage("Título precisa ter no máximo 120 caracteres");
    
        RuleFor(r => r.Subtitle).Must(SubtitleValidate)
            .WithMessage("Subtítulo precisa ter no mínimo 3 caracteres")
            .MaximumLength(120).WithMessage("Subtítulo precisa ter no máximo 120 caracteres");

        RuleFor(r => r.Content).NotEmpty().WithMessage("Contéudo não pode ser vazio")
            .MaximumLength(2000).WithMessage("O conteúdo precisa ter no máximo 2000 caracteres"); 
         
    }

    private bool SubtitleValidate(string subtitle)
    {
        if(string.IsNullOrEmpty(subtitle))
        {
            return (subtitle.Length < 3); 
        }
        return false; 
    }
}

