using ErrorOr;
using MediatR;
using Methaq.Domain.AttendanceRecords.enums;

namespace Methaq.Application.AttendanceRecords.Commands.UpdateAttendance;

public record UpdateAttendanceCommand(
    Guid AttendanceRecordId,
    AttendanceStatus Status,
    string? ExcuseReason,
    string? Notes
) : IRequest<ErrorOr<Success>>;