using ErrorOr;
using System;

namespace Methaq.Domain.LeaveRequests;

public static class LeaveRequestErrors
{
    public static readonly Error InvalidDateRange = Error.Validation(
        code: "LeaveRequest.Dates",
        description: "Start date must be before end date.");

    public static readonly Error ReasonRequired = Error.Validation(
        code: "LeaveRequest.Reason",
        description: "Reason for leave is required.");

    public static readonly Error NotPending = Error.Conflict(
        code: "LeaveRequest.NotPending",
        description: "Only pending requests can be approved.");

    public static readonly Error RejectReasonRequired = Error.Validation(
        code: "LeaveRequest.RejectReason",
        description: "Rejection reason must be provided.");

    public static readonly Error CannotUpdateNonPending = Error.Conflict(
        code: "LeaveRequest.UpdateNotPending",
        description: "Cannot update non-pending request.");
}
