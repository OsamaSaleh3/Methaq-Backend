using FluentValidation;

namespace Methaq.Application.GroupChats.Commands.SendMessage;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.GroupChatId)
            .NotEmpty().WithMessage("Group chat ID is required.");

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Sender ID is required.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Message content is required.")
            .MaximumLength(2000).WithMessage("Message must not exceed 2000 characters.");
    }
}