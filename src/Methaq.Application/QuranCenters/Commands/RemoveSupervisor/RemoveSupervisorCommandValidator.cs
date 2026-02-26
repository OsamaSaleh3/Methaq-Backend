using FluentValidation;

namespace Methaq.Application.QuranCenters.Commands.RemoveSupervisor;

public class RemoveSupervisorCommandValidator : AbstractValidator<RemoveSupervisorCommand>
{
    public RemoveSupervisorCommandValidator()
    {
        RuleFor(x => x.CenterId)
            .NotEmpty().WithMessage("Center ID is required.");

        RuleFor(x => x.SupervisorId)
            .NotEmpty().WithMessage("Supervisor ID is required.");
    }
}