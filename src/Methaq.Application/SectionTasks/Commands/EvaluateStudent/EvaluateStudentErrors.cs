using ErrorOr;

namespace Methaq.Application.SectionTasks.Commands.EvaluateStudent;

public static class EvaluateStudentErrors
{
    public static readonly Error TaskNotFound = Error.NotFound(
        code: "SectionTask.NotFound",
        description: "Section task not found.");

    public static readonly Error StudentNotInSection = Error.Conflict(
        code: "SectionTask.StudentNotInSection",
        description: "Student is not enrolled in this section.");
}