using System.Data;
using BlogMVC.Application.Dtos.User;
using FluentValidation;

namespace BlogMVC.Application.Validators;

public class CreateAccountValidator : AbstractValidator<CreateAccountRequestJson>
{
    public CreateAccountValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("Nome é requirido") 
            .MinimumLength(3).WithMessage("Nome precisa ter no mínimo 3 caracteres") 
            .MaximumLength(30).WithMessage("Nome deve ter no máximo 30 caracteres");

        RuleFor(r => r.Surname).NotEmpty().WithMessage("Sobrenome é requirido") 
            .MinimumLength(3).WithMessage("Sobrenome precisa ter no mínimo 3 caracteres") 
            .MaximumLength(30).WithMessage("Sobrenome deve ter no máximo 30 caracteres"); 
        
        RuleFor(r => r.Email).NotEmpty().WithMessage("Email é requirido");
        When(r => !string.IsNullOrWhiteSpace(r.Email), () => 
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage("Endereço de email inválido"); 
        });

        RuleFor(r => r.Password).NotEmpty().WithMessage("Senha é requirido");
        When(r => !string.IsNullOrWhiteSpace(r.Password), () => 
        {
            RuleFor(c => c.Password).MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres");
        });


       
    
    }
}
