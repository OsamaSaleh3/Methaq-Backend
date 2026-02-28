using ErrorOr;
using MediatR;
using Methaq.Domain.AttendanceRecords.enums;

namespace Methaq.Application.AttendanceRecords.Queries.GetAttendanceByLecture;

public record GetAttendanceByLectureQuery(Guid LectureId)
    : IRequest<ErrorOr<List<AttendanceByLectureResponse>>>;

public record AttendanceByLectureResponse(
    Guid Id,
    Guid StudentId,
    string StudentName,
    AttendanceStatus Status,
    string? ExcuseReason,
    string? Notes,
    DateTime CreatedAt
);