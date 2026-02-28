using ErrorOr;
using MediatR;
using Methaq.Domain.AttendanceRecords.enums;

namespace Methaq.Application.AttendanceRecords.Queries.GetAttendanceByStudent;

public record GetAttendanceByStudentQuery(Guid StudentId)
    : IRequest<ErrorOr<List<AttendanceByStudentResponse>>>;

public record AttendanceByStudentResponse(
    Guid Id,
    Guid LectureId,
    DateTime LectureDate,
    AttendanceStatus Status,
    string? ExcuseReason,
    string? Notes,
    DateTime CreatedAt
);