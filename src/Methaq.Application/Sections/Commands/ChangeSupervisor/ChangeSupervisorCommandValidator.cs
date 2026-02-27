using FluentValidation;

namespace Methaq.Application.Sections.Commands.ChangeSupervisor;

public class ChangeSupervisorCommandValidator : AbstractValidator<ChangeSupervisorCommand>
{
    public ChangeSupervisorCommandValidator()
    {
        RuleFor(x => x.SectionId)
            .NotEmpty().WithMessage("Section ID is required.");

        RuleFor(x => x.NewSupervisorId)
            .NotEmpty().WithMessage("New supervisor ID is required.");
    }
}