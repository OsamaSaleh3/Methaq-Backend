using FluentValidation;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.RejectRequest;

public class RejectRequestCommandValidator : AbstractValidator<RejectRequestCommand>
{
    public RejectRequestCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithMessage("Request ID is required.");
    }
}