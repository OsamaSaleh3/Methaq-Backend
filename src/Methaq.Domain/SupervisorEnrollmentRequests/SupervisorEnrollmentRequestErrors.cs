using ErrorOr;

namespace Methaq.Domain.SupervisorEnrollmentRequests;

public static class SupervisorEnrollmentRequestErrors
{
    public static readonly Error EmployeeIdRequired = Error.Validation(
        code: "SupervisorRequest.EmployeeId",
        description: "Employee ID is required.");

    public static readonly Error CenterIdRequired = Error.Validation(
        code: "SupervisorRequest.CenterId",
        description: "Center ID is required.");

    public static readonly Error AlreadyReviewed = Error.Conflict(
        code: "SupervisorRequest.AlreadyReviewed",
        description: "This request has already been reviewed.");

    public static readonly Error NotFound = Error.NotFound(
        code: "SupervisorRequest.NotFound",
        description: "Request not found.");

    public static readonly Error AlreadyPending = Error.Conflict(
        code: "SupervisorRequest.AlreadyPending",
        description: "You already have a pending request for this center.");

    public static readonly Error AlreadyInCenter = Error.Conflict(
        code: "SupervisorRequest.AlreadyInCenter",
        description: "Employee is already assigned to a center.");

    public static readonly Error NotCenterManager = Error.Forbidden(
        code: "SupervisorRequest.NotCenterManager",
        description: "Only the center manager can review requests.");
}