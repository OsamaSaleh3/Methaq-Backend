namespace Methaq.Contracts.SectionTasks;

public record EvaluateStudentRequest(
    Guid StudentId,
    decimal AchievedMark,
    string? Notes
);