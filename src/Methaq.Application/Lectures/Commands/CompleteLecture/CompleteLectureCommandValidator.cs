using FluentValidation;

namespace Methaq.Application.Lectures.Commands.CompleteLecture;

public class CompleteLectureCommandValidator : AbstractValidator<CompleteLectureCommand>
{
    public CompleteLectureCommandValidator()
    {
        RuleFor(x => x.LectureId)
            .NotEmpty().WithMessage("Lecture ID is required.");

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Notes));
    }
}