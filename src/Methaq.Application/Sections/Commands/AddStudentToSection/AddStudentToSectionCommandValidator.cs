using FluentValidation;

namespace Methaq.Application.Sections.Commands.AddStudentToSection;

public class AddStudentToSectionCommandValidator : AbstractValidator<AddStudentToSectionCommand>
{
    public AddStudentToSectionCommandValidator()
    {
        RuleFor(x => x.SectionId)
            .NotEmpty().WithMessage("Section ID is required.");

        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required.");
    }
}