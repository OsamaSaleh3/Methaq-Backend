using FluentValidation;

namespace Methaq.Application.Lectures.Commands.StartLecture;

public class StartLectureCommandValidator : AbstractValidator<StartLectureCommand>
{
    public StartLectureCommandValidator()
    {
        RuleFor(x => x.LectureId)
            .NotEmpty().WithMessage("Lecture ID is required.");
    }
}