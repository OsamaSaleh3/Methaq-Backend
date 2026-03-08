using FluentValidation;

namespace Methaq.Application.GroupChats.Commands.DeleteMessage;

public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
{
    public DeleteMessageCommandValidator()
    {
        RuleFor(x => x.MessageId)
            .NotEmpty().WithMessage("Message ID is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}