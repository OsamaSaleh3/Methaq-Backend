using ErrorOr;

namespace Methaq.Application.SectionTasks.Commands.CreateSectionTask;

public static class CreateSectionTaskErrors
{
    public static readonly Error LectureNotFound = Error.NotFound(
        code: "SectionTask.LectureNotFound",
        description: "Lecture not found.");

    public static readonly Error LectureCancelled = Error.Conflict(
        code: "SectionTask.LectureCancelled",
        description: "Cannot create a task for a cancelled lecture.");

    public static readonly Error SectionNotFound = Error.NotFound(
        code: "SectionTask.SectionNotFound",
        description: "Section not found.");

    public static readonly Error SectionClosed = Error.Conflict(
        code: "SectionTask.SectionClosed",
        description: "Cannot create a task for a closed section.");

    public static readonly Error StudentNotInSection = Error.Conflict(
        code: "SectionTask.StudentNotInSection",
        description: "Student is not enrolled in this section.");

    public static readonly Error EmployeeNotFound = Error.NotFound(
        code: "SectionTask.EmployeeNotFound",
        description: "Employee not found.");
}