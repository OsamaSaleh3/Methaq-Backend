using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.AttendanceRecords.Queries.GetAttendanceByStudent;

public class GetAttendanceByStudentQueryHandler
    : IRequestHandler<GetAttendanceByStudentQuery, ErrorOr<List<AttendanceByStudentResponse>>>
{
    private readonly IAttendanceRecordRepository _attendanceRepository;

    public GetAttendanceByStudentQueryHandler(IAttendanceRecordRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<ErrorOr<List<AttendanceByStudentResponse>>> Handle(
        GetAttendanceByStudentQuery query,
        CancellationToken cancellationToken)
    {
        var records = await _attendanceRepository.GetByStudentIdAsync(query.StudentId);

        return records.Select(r => new AttendanceByStudentResponse(
            r.Id,
            r.LectureId,
            r.Lecture.Date,
            r.Status,
            r.ExcuseReason,
            r.Notes,
            r.CreatedAt
        )).ToList();
    }
}