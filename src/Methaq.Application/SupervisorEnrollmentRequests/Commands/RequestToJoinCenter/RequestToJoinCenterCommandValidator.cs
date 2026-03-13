using FluentValidation;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.RequestToJoinCenter;

public class RequestToJoinCenterCommandValidator : AbstractValidator<RequestToJoinCenterCommand>
{
    public RequestToJoinCenterCommandValidator()
    {
        RuleFor(x => x.CenterId)
            .NotEmpty().WithMessage("Center ID is required.");
    }
}