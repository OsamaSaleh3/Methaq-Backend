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
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int Status,
    string? Notes,
    int AttendanceCount,
    int TasksCount
);