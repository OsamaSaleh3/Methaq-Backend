using ErrorOr;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.RequestToJoinCenter;

public static class RequestToJoinCenterErrors
{
    public static readonly Error EmployeeNotFound = Error.NotFound(
        code: "RequestToJoinCenter.EmployeeNotFound",
        description: "Employee not found.");

    public static readonly Error CenterNotFound = Error.NotFound(
        code: "RequestToJoinCenter.CenterNotFound",
        description: "Center not found.");
}