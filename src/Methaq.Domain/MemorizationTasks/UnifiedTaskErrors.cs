using ErrorOr;

namespace Methaq.Domain.MemorizationTasks;

public static class UnifiedTaskErrors
{
    public static readonly Error TitleRequired = Error.Validation(
        code: "UnifiedTask.Title",
        description: "Task title is required.");

    public static readonly Error SectionIdRequired = Error.Validation(
        code: "UnifiedTask.SectionId",
        description: "Section ID is required.");

    public static readonly Error LectureIdRequired = Error.Validation(
        code: "UnifiedTask.LectureId",
        description: "Lecture ID is required.");

    public static readonly Error AssignedByIdRequired = Error.Validation(
        code: "UnifiedTask.AssignedById",
        description: "AssignedBy ID is required.");

    public static readonly Error InvalidFullMark = Error.Validation(
        code: "UnifiedTask.FullMark",
        description: "Full mark must be greater than zero.");

    public static Error InvalidMark(decimal fullMark) => Error.Validation(
        code: "UnifiedTask.Mark",
        description: $"Mark must be between 0 and {fullMark}.");
}
