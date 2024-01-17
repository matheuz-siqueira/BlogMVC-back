using BlogMVC.Application.Dtos.User;
using FluentValidation;

namespace BlogMVC.Application.Validators;

public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordRequestJson>
{
    public UpdatePasswordValidator()
    {
        RuleFor(r => r.NewPassword).SetValidator(new PasswordValidator());
    }
}
