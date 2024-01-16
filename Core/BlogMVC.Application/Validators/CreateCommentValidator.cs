using BlogMVC.Application.Dtos.Comment;
using FluentValidation;

namespace BlogMVC.Application.Validators;

public class CreateCommentValidator : AbstractValidator<CreateCommentRequestJson>
{
    public CreateCommentValidator()
    {
        RuleFor(r => r.Commentary).NotEmpty().WithMessage("Comentário não pode ser vazio")
            .MinimumLength(3).WithMessage("Comentário precisa ter no mínimo 3 caracteres");
    }
}
