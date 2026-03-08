using FluentValidation;

namespace Methaq.Application.GroupChats.Commands.EditMessage;

public class EditMessageCommandValidator : AbstractValidator<EditMessageCommand>
{
    public EditMessageCommandValidator()
    {
        RuleFor(x => x.MessageId)
            .NotEmpty().WithMessage("Message ID is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.NewContent)
            .NotEmpty().WithMessage("Message content is required.")
            .MaximumLength(2000).WithMessage("Message must not exceed 2000 characters.");
    }
}