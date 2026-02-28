using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.AttendanceRecords.Queries.GetAttendanceByLecture;

public class GetAttendanceByLectureQueryHandler
    : IRequestHandler<GetAttendanceByLectureQuery, ErrorOr<List<AttendanceByLectureResponse>>>
{
    private readonly IAttendanceRecordRepository _attendanceRepository;

    public GetAttendanceByLectureQueryHandler(IAttendanceRecordRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<ErrorOr<List<AttendanceByLectureResponse>>> Handle(
        GetAttendanceByLectureQuery query,
        CancellationToken cancellationToken)
    {
        var records = await _attendanceRepository.GetByLectureIdAsync(query.LectureId);

        return records.Select(r => new AttendanceByLectureResponse(
            r.Id,
            r.StudentId,
            r.Student.User.FullName,
            r.Status,
            r.ExcuseReason,
            r.Notes,
            r.CreatedAt
        )).ToList();
    }
}