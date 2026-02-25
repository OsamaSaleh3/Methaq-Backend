using FluentValidation;

namespace Methaq.Application.Auth.Commands.ApproveAccount;

public class ApproveAccountCommandValidator : AbstractValidator<ApproveAccountCommand>
{
    public ApproveAccountCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}