using ErrorOr;
using MediatR;

public record AddStudentReportCommand(
    Guid FinalReportId,
    Guid StudentId,
    decimal ParticipationScore,
    decimal BehaviorScore,
    string? SupervisorNotes
) : IRequest<ErrorOr<Success>>;