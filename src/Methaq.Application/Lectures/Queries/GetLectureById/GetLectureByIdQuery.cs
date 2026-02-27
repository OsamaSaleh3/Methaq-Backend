using ErrorOr;
using MediatR;

namespace Methaq.Application.Lectures.Queries.GetLectureById;

public record GetLectureByIdQuery(
    Guid LectureId
) : IRequest<ErrorOr<LectureDetailsResponse>>;

public record LectureDetailsResponse(
    Guid Id,
    Guid SectionId,
    string SectionName,
    DateTime Date,
    string StartTime,
    string EndTime,
    string Status,
    string? Notes,
    int AttendanceCount,
    int TasksCount
);