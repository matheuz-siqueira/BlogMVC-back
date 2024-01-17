using FluentValidation;

namespace BlogMVC.Application.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(r => r).NotEmpty().WithMessage("Senha é requirido");
        When(r => !string.IsNullOrWhiteSpace(r), () => 
        {
            RuleFor(c => c).MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres");
        });
    }
}
