using FluentValidation;

namespace Methaq.Application.EnrollmentRequests.Commands.SubmitEnrollmentRequest;

public class SubmitEnrollmentRequestCommandValidator : AbstractValidator<SubmitEnrollmentRequestCommand>
{
    public SubmitEnrollmentRequestCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required.");

        RuleFor(x => x.CenterId)
            .NotEmpty().WithMessage("Center ID is required.");
    }
}