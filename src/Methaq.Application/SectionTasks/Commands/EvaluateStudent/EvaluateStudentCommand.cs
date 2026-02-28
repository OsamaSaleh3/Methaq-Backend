using ErrorOr;
using MediatR;

namespace Methaq.Application.SectionTasks.Commands.EvaluateStudent;

public record EvaluateStudentCommand(
    Guid SectionTaskId,
    Guid StudentId,
    decimal AchievedMark,
    string? Notes
) : IRequest<ErrorOr<Success>>;