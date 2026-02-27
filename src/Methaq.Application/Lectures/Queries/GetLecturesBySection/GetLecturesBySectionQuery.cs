using ErrorOr;
using MediatR;

namespace Methaq.Application.Lectures.Queries.GetLecturesBySection;

public record GetLecturesBySectionQuery(
    Guid SectionId
) : IRequest<ErrorOr<List<LectureSummaryResponse>>>;

public record LectureSummaryResponse(
    Guid Id,
    DateTime Date,
    string StartTime,
    string EndTime,
    string Status,
    int AttendanceCount,
    int TasksCount
);