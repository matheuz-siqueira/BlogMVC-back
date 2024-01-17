using BlogMVC.Application.Dtos.Auth;
using FluentValidation;

namespace BlogMVC.Application.Validators;

public class LoginValidator : AbstractValidator<LoginRequestJson>
{
    public LoginValidator()
    {
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
