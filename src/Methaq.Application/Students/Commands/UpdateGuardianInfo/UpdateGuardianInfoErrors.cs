using ErrorOr;

namespace Methaq.Application.UseCases.Students.Commands.UpdateGuardianInfo;

public static class UpdateGuardianInfoErrors
{
    public static readonly Error StudentNotFound = Error.NotFound(
        code: "UpdateGuardianInfo.StudentNotFound",
        description: "Student not found.");
}