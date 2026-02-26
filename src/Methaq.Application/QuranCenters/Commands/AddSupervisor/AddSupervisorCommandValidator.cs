using FluentValidation;

namespace Methaq.Application.QuranCenters.Commands.AddSupervisor;

public class AddSupervisorCommandValidator : AbstractValidator<AddSupervisorCommand>
{
    public AddSupervisorCommandValidator()
    {
        RuleFor(x => x.CenterId)
            .NotEmpty().WithMessage("Center ID is required.");

        RuleFor(x => x.SupervisorId)
            .NotEmpty().WithMessage("Supervisor ID is required.");
    }
}