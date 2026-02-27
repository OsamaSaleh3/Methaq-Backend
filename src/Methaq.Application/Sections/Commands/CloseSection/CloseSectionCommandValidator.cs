using FluentValidation;

namespace Methaq.Application.Sections.Commands.CloseSection;

public class CloseSectionCommandValidator : AbstractValidator<CloseSectionCommand>
{
    public CloseSectionCommandValidator()
    {
        RuleFor(x => x.SectionId)
            .NotEmpty().WithMessage("Section ID is required.");
    }
}