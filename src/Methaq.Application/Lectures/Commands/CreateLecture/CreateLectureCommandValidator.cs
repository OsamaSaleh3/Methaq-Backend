using FluentValidation;
using Methaq.Domain.Lectures;

namespace Methaq.Application.Lectures.Commands.CreateLecture;

public class CreateLectureCommandValidator : AbstractValidator<CreateLectureCommand>
{
    public CreateLectureCommandValidator()
    {
        RuleFor(x => x.SectionId)
            .NotEmpty().WithMessage("Section ID is required.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .Must(date => date >= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date cannot be in the past.");

        

        RuleFor(x => x.EndTime)
            .Must((command, endTime) => endTime > command.StartTime)
            .WithMessage("End time must be after start time.");
    }
}