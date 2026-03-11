using ErrorOr;
using MediatR;
using Methaq.Domain.SectionTasks.enums;

namespace Methaq.Application.UseCases.Students.Queries.GetMyTasks;

public record GetMyTasksQuery(string UserId) : IRequest<ErrorOr<List<StudentTaskResponse>>>;

public record StudentTaskResponse(
    Guid Id,
    string Title,
    string? Description,
    TaskTypes Type,
    Domain.SectionTasks.enums.TaskStatus Status,
    decimal FullMark,
    decimal? AchievedMark);