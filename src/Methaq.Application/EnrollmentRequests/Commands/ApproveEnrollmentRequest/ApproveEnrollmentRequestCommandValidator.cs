using FluentValidation;

namespace Methaq.Application.EnrollmentRequests.Commands.ApproveEnrollmentRequest;

public class ApproveEnrollmentRequestCommandValidator : AbstractValidator<ApproveEnrollmentRequestCommand>
{
    public ApproveEnrollmentRequestCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithMessage("Request ID is required.");
    }
}