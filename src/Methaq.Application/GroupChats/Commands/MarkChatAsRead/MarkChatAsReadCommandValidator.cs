using FluentValidation;

namespace Methaq.Application.GroupChats.Commands.MarkChatAsRead;

public class MarkChatAsReadCommandValidator : AbstractValidator<MarkChatAsReadCommand>
{
    public MarkChatAsReadCommandValidator()
    {
        RuleFor(x => x.GroupChatId)
            .NotEmpty().WithMessage("Group chat ID is required.");

        RuleFor(x => x.LastMessageId)
            .NotEmpty().WithMessage("Last message ID is required.");
    }
}