using FluentValidation;

namespace Methaq.Application.UseCases.Students.Commands.RemoveStudentFromCenter;

public class RemoveStudentFromCenterCommandValidator : AbstractValidator<RemoveStudentFromCenterCommand>
{
    public RemoveStudentFromCenterCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required.");
    }
}