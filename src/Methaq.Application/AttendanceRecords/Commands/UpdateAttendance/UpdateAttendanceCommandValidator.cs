using FluentValidation;
using Methaq.Domain.AttendanceRecords.enums;

namespace Methaq.Application.AttendanceRecords.Commands.UpdateAttendance;

public class UpdateAttendanceCommandValidator : AbstractValidator<UpdateAttendanceCommand>
{
    public UpdateAttendanceCommandValidator()
    {
        RuleFor(x => x.AttendanceRecordId)
            .NotEmpty().WithMessage("Attendance Record ID is required.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid attendance status.");

        RuleFor(x => x.ExcuseReason)
            .NotEmpty().WithMessage("Excuse reason is required when status is Excused.")
            .When(x => x.Status == AttendanceStatus.Excused);
    }
}