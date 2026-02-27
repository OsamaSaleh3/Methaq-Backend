using FluentValidation;

namespace Methaq.Application.Sections.Commands.RemoveStudentFromSection;

public class RemoveStudentFromSectionCommandValidator : AbstractValidator<RemoveStudentFromSectionCommand>
{
    public RemoveStudentFromSectionCommandValidator()
    {
        RuleFor(x => x.SectionId)
            .NotEmpty().WithMessage("Section ID is required.");

        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required.");
    }
}