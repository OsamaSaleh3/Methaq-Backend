using ErrorOr;

namespace Methaq.Domain.CenterEnrollmentRequests;

public static class CenterEnrollmentRequestErrors
{
    public static readonly Error StudentIdRequired = Error.Validation(
        code: "EnrollmentRequest.StudentId",
        description: "Student ID is required.");

    public static readonly Error CenterIdRequired = Error.Validation(
        code: "EnrollmentRequest.CenterId",
        description: "Center ID is required.");

    public static readonly Error AlreadyReviewed = Error.Conflict(
        code: "EnrollmentRequest.AlreadyReviewed",
        description: "This request has already been reviewed.");
}
