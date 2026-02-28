using FluentValidation;

namespace Methaq.Application.SectionTasks.Commands.CreateSectionTask;

public class CreateSectionTaskCommandValidator : AbstractValidator<CreateSectionTaskCommand>
{
    public CreateSectionTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.SectionId)
            .NotEmpty().WithMessage("Section ID is required.");

        RuleFor(x => x.LectureId)
            .NotEmpty().WithMessage("Lecture ID is required.");

        RuleFor(x => x.AssignedById)
            .NotEmpty().WithMessage("AssignedBy ID is required.");

        RuleFor(x => x.FullMark)
            .GreaterThan(0).WithMessage("Full mark must be greater than zero.");

        RuleFor(x => x.Types)
            .IsInEnum().WithMessage("Invalid task type.");
    }
}