using FluentValidation;

namespace Methaq.Application.UseCases.Notifications.Commands.MarkAllAsRead;

public class MarkAllAsReadCommandValidator : AbstractValidator<MarkAllAsReadCommand>
{
    public MarkAllAsReadCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}