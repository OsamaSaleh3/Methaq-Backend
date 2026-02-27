using FluentValidation;

namespace Methaq.Application.EnrollmentRequests.Commands.RejectEnrollmentRequest;

public class RejectEnrollmentRequestCommandValidator : AbstractValidator<RejectEnrollmentRequestCommand>
{
    public RejectEnrollmentRequestCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithMessage("Request ID is required.");

        RuleFor(x => x.Reason)
            .MaximumLength(300).WithMessage("Reason cannot exceed 300 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));
    }
}