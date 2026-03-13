using ErrorOr;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.ApproveRequest;

public static class ApproveRequestErrors
{
    public static readonly Error ManagerNotFound = Error.NotFound(
        code: "ApproveRequest.ManagerNotFound",
        description: "Manager not found.");
}