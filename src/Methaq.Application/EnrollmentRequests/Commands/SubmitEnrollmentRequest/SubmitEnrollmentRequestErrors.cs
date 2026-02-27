using ErrorOr;

namespace Methaq.Application.EnrollmentRequests.Commands.SubmitEnrollmentRequest;

public static class SubmitEnrollmentRequestErrors
{
    public static readonly Error StudentNotFound = Error.NotFound(
        code: "EnrollmentRequest.StudentNotFound",
        description: "Student not found.");

    public static readonly Error CenterNotFound = Error.NotFound(
        code: "EnrollmentRequest.CenterNotFound",
        description: "Center not found.");

    public static readonly Error AlreadyEnrolled = Error.Conflict(
        code: "EnrollmentRequest.AlreadyEnrolled",
        description: "Student already has an active enrollment request for this center.");
}