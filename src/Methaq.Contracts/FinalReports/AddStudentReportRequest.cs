namespace Methaq.Contracts.FinalReports;

public record AddStudentReportRequest(
    Guid StudentId,
    decimal ParticipationScore,
    decimal BehaviorScore,
    string? SupervisorNotes
);