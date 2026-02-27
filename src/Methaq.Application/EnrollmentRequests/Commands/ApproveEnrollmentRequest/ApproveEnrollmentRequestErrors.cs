using ErrorOr;

namespace Methaq.Application.EnrollmentRequests.Commands.ApproveEnrollmentRequest;

public static class ApproveEnrollmentRequestErrors
{
    public static readonly Error RequestNotFound = Error.NotFound(
        code: "EnrollmentRequest.NotFound",
        description: "Enrollment request not found.");

    public static readonly Error AlreadyProcessed = Error.Conflict(
        code: "EnrollmentRequest.AlreadyProcessed",
        description: "Enrollment request has already been processed.");
}