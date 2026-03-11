using ErrorOr;

namespace Methaq.Application.UseCases.Students.Commands.RemoveStudentFromCenter;

public static class RemoveStudentFromCenterErrors
{
    public static readonly Error StudentNotFound = Error.NotFound(
        code: "RemoveStudentFromCenter.StudentNotFound",
        description: "Student not found.");
}