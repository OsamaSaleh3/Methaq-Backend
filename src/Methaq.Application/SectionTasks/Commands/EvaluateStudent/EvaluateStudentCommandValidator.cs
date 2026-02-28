using FluentValidation;

namespace Methaq.Application.SectionTasks.Commands.EvaluateStudent;

public class EvaluateStudentCommandValidator : AbstractValidator<EvaluateStudentCommand>
{
    public EvaluateStudentCommandValidator()
    {
        RuleFor(x => x.SectionTaskId)
            .NotEmpty().WithMessage("Section Task ID is required.");

        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required.");

        RuleFor(x => x.AchievedMark)
            .GreaterThanOrEqualTo(0).WithMessage("Achieved mark must be greater than or equal to zero.");
    }
}