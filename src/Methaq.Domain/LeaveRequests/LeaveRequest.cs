using ErrorOr;
using Methaq.Domain.LeaveRequests.enums;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;
using System;

namespace Methaq.Domain.LeaveRequests;

public class LeaveRequest : BaseEntity
{
    public Guid RequesterUserId { get; private set; }
    public ApplicationUser Requester { get; private set; } = null!;

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Reason { get; private set; } = null!;
    public LeaveRequestStatus Status { get; private set; }
    public string? AdminNotes { get; private set; }

    protected LeaveRequest() { }

    private LeaveRequest(Guid requesterId, DateTime start, DateTime end, string reason)
    {
        RequesterUserId = requesterId;
        StartDate = start;
        EndDate = end;
        Reason = reason;
        Status = LeaveRequestStatus.Pending;
    }

    public static ErrorOr<LeaveRequest> Create(Guid requesterId, DateTime start, DateTime end, string reason)
    {
        if (start >= end)
            return LeaveRequestErrors.InvalidDateRange;

        if (string.IsNullOrWhiteSpace(reason))
            return LeaveRequestErrors.ReasonRequired;

        return new LeaveRequest(requesterId, start, end, reason);
    }

    public ErrorOr<Success> Approve(string? notes = null)
    {
        if (Status != LeaveRequestStatus.Pending)
            return LeaveRequestErrors.NotPending;

        Status = LeaveRequestStatus.Approved;
        AdminNotes = notes;
        MarkAsUpdated();

        return Result.Success;
    }

    public ErrorOr<Success> Reject(string notes)
    {
        if (Status != LeaveRequestStatus.Pending)
            return LeaveRequestErrors.NotPending;

        if (string.IsNullOrWhiteSpace(notes))
            return LeaveRequestErrors.RejectReasonRequired;

        Status = LeaveRequestStatus.Rejected;
        AdminNotes = notes;
        MarkAsUpdated();

        return Result.Success;
    }

    public int GetLeaveDays() => (int)EndDate.Subtract(StartDate).TotalDays + 1;

    public bool IsExpired() => EndDate < DateTime.UtcNow && Status == LeaveRequestStatus.Pending;
    public ErrorOr<Success> UpdateReason(string newReason)
    {
        if (Status != LeaveRequestStatus.Pending)
            return LeaveRequestErrors.CannotUpdateNonPending;

        if (string.IsNullOrWhiteSpace(newReason))
            return LeaveRequestErrors.ReasonRequired;

        Reason = newReason;
        MarkAsUpdated();
        return Result.Success;
    }

    
}