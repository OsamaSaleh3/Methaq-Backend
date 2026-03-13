using ErrorOr;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.RejectRequest;

public static class RejectRequestErrors
{
    public static readonly Error ManagerNotFound = Error.NotFound(
        code: "RejectRequest.ManagerNotFound",
        description: "Manager not found.");
}