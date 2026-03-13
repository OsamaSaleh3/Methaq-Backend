using FluentValidation;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.ApproveRequest;

public class ApproveRequestCommandValidator : AbstractValidator<ApproveRequestCommand>
{
    public ApproveRequestCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithMessage("Request ID is required.");
    }
}