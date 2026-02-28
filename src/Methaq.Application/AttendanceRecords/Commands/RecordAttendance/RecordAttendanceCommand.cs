using ErrorOr;
using MediatR;
using Methaq.Domain.AttendanceRecords.enums;

namespace Methaq.Application.AttendanceRecords.Commands.RecordAttendance;

public record RecordAttendanceCommand(
    Guid LectureId,
    Guid StudentId,
    AttendanceStatus Status,
    string? ExcuseReason,
    string? Notes
) : IRequest<ErrorOr<Guid>>;