using FluentValidation;

namespace Methaq.Application.Sections.Commands.CreateSection;

public class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
{
    public CreateSectionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Section name is required.")
            .MaximumLength(100);

        RuleFor(x => x.AcademicLevel)
            .IsInEnum().WithMessage("Invalid academic level.");

        RuleFor(x => x.CenterId)
            .NotEmpty().WithMessage("Center ID is required.");

        RuleFor(x => x.SupervisorId)
            .NotEmpty().WithMessage("Supervisor ID is required.");

        RuleFor(x => x.ScheduleDays)
            .NotEmpty().WithMessage("Schedule days are required.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required.")
            .Must((command, endTime) => endTime > command.StartTime)
            .WithMessage("End time must be after start time.");
    }
}