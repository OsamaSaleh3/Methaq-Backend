using FluentValidation;
using Methaq.Domain.AttendanceRecords.enums;

namespace Methaq.Application.AttendanceRecords.Commands.RecordAttendance;

public class RecordAttendanceCommandValidator : AbstractValidator<RecordAttendanceCommand>
{
    public RecordAttendanceCommandValidator()
    {
        RuleFor(x => x.LectureId)
            .NotEmpty().WithMessage("Lecture ID is required.");

        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid attendance status.");

        RuleFor(x => x.ExcuseReason)
            .NotEmpty().WithMessage("Excuse reason is required when status is Excused.")
            .When(x => x.Status == AttendanceStatus.Excused);
    }
}