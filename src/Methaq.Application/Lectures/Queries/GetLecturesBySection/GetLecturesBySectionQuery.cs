using ErrorOr;
using MediatR;

namespace Methaq.Application.Lectures.Queries.GetLecturesBySection;

public record GetLecturesBySectionQuery(
    Guid SectionId
) : IRequest<ErrorOr<List<LectureSummaryResponse>>>;

public record LectureSummaryResponse(
    Guid Id,
    DateTime Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int Status,
    int AttendanceCount,
    int TasksCount
);