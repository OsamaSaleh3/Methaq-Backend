using FluentValidation;

namespace Methaq.Application.FinalReports.Commands.AddStudentReport;

public class AddStudentReportCommandValidator : AbstractValidator<AddStudentReportCommand>
{
    public AddStudentReportCommandValidator()
    {
        RuleFor(x => x.FinalReportId)
            .NotEmpty().WithMessage("Final Report ID is required.");

        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required.");

        RuleFor(x => x.ParticipationScore)
            .InclusiveBetween(0, 100).WithMessage("Participation score must be between 0 and 100.");

        RuleFor(x => x.BehaviorScore)
            .InclusiveBetween(0, 100).WithMessage("Behavior score must be between 0 and 100.");

        RuleFor(x => x.SupervisorNotes)
            .MaximumLength(500).WithMessage("Supervisor notes must not exceed 500 characters.")
            .When(x => x.SupervisorNotes is not null);
    }
}