using FluentValidation;

namespace Methaq.Application.Lectures.Commands.CancelLecture;

public class CancelLectureCommandValidator : AbstractValidator<CancelLectureCommand>
{
    public CancelLectureCommandValidator()
    {
        RuleFor(x => x.LectureId)
            .NotEmpty().WithMessage("Lecture ID is required.");
    }
}