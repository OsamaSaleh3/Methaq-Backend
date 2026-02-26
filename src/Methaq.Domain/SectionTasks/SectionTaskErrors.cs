using ErrorOr;

namespace Methaq.Domain.SectionTasks;

public static class SectionTaskErrors
{
    public static readonly Error TitleRequired = Error.Validation(
        code: "SectionTask.Title",
        description: "Task title is required.");

    public static readonly Error SectionIdRequired = Error.Validation(
        code: "SectionTask.SectionId",
        description: "Section ID is required.");

    public static readonly Error LectureIdRequired = Error.Validation(
        code: "SectionTask.LectureId",
        description: "Lecture ID is required.");

    public static readonly Error AssignedByIdRequired = Error.Validation(
        code: "SectionTask.AssignedById",
        description: "AssignedBy ID is required.");

    public static readonly Error InvalidFullMark = Error.Validation(
        code: "SectionTask.FullMark",
        description: "Full mark must be greater than zero.");

    public static Error InvalidMark(decimal fullMark) => Error.Validation(
        code: "SectionTask.Mark",
        description: $"Mark must be between 0 and {fullMark}.");
}
