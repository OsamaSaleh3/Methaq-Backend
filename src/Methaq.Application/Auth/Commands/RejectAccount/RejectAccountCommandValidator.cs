using FluentValidation;

namespace Methaq.Application.Auth.Commands.RejectAccount;

public class RejectAccountCommandValidator : AbstractValidator<RejectAccountCommand>
{
    public RejectAccountCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Reason)
            .MaximumLength(300).WithMessage("Reason cannot exceed 300 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));
    }
}