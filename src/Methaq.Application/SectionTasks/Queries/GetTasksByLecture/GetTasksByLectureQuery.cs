using ErrorOr;
using MediatR;
using Methaq.Domain.SectionTasks.enums;
using Methaq.Domain.SectionTasks.ValueObject;

namespace Methaq.Application.SectionTasks.Queries.GetTasksByLecture;

public record GetTasksByLectureQuery(Guid LectureId)
    : IRequest<ErrorOr<List<SectionTaskResponse>>>;

public record SectionTaskResponse(
    Guid Id,
    string Title,
    string? Description,
    Guid SectionId,
    Guid LectureId,
    Guid AssignedById,
    string AssignedByName,
    decimal FullMark,
    TaskTypes Types,
    Domain.SectionTasks.enums.TaskStatus Status,
    Guid? StudentId,
    string? StudentName,
    QuranRange? Range,
    DateTime CreatedAt
);