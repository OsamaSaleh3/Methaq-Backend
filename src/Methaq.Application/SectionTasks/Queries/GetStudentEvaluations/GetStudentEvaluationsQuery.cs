using ErrorOr;
using MediatR;

namespace Methaq.Application.SectionTasks.Queries.GetStudentEvaluations;

public record GetStudentEvaluationsQuery(Guid StudentId)
    : IRequest<ErrorOr<List<StudentEvaluationResponse>>>;

public record StudentEvaluationResponse(
    Guid SectionTaskId,
    string TaskTitle,
    decimal FullMark,
    decimal AchievedMark,
    string? Notes,
    DateTime EvaluatedAt
);