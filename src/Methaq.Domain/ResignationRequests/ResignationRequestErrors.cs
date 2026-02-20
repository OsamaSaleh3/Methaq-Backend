using ErrorOr;
using System;

namespace Methaq.Domain.ResignationRequests;

public static class ResignationRequestErrors
{
    public static readonly Error EmployeeIdRequired = Error.Validation(
        code: "ResignationRequest.EmployeeId",
        description: "Employee ID is required.");

    public static readonly Error ReasonRequired = Error.Validation(
        code: "ResignationRequest.Reason",
        description: "Reason for resignation is required.");

    public static readonly Error NotPending = Error.Conflict(
        code: "ResignationRequest.NotPending",
        description: "Only pending requests can be approved.");

    public static readonly Error RejectReasonRequired = Error.Validation(
        code: "ResignationRequest.RejectReason",
        description: "Rejection reason must be provided.");
}
