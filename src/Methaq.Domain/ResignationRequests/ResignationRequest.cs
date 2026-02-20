using ErrorOr;
using Methaq.Domain.Employees;
using Methaq.Domain.Common;
using Methaq.Domain.ResignationRequests.enums;
using System;

namespace Methaq.Domain.ResignationRequests;

public class ResignationRequest : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; } = null!;

    public string Reason { get; private set; } = null!;
    public ResignationRequestStatus Status { get; private set; }

    public Guid? ProcessedById { get; private set; }
    public Employee? ProcessedBy { get; private set; }

    public DateTime? ProcessedAt { get; private set; }
    public string? ManagerNotes { get; private set; }

    protected ResignationRequest() { }

    private ResignationRequest(Guid employeeId, string reason)
    {
        EmployeeId = employeeId;
        Reason = reason;
        Status = ResignationRequestStatus.Pending;
    }

    public static ErrorOr<ResignationRequest> Create(Guid employeeId, string reason)
    {
        if (employeeId == Guid.Empty)
            return ResignationRequestErrors.EmployeeIdRequired;

        if (string.IsNullOrWhiteSpace(reason))
            return ResignationRequestErrors.ReasonRequired;

        return new ResignationRequest(employeeId, reason);
    }

    public ErrorOr<Success> Approve(Guid processedById, string? managerNotes = null)
    {
        if (Status != ResignationRequestStatus.Pending)
            return ResignationRequestErrors.NotPending;

        Status = ResignationRequestStatus.Approved;
        ProcessedById = processedById;
        ProcessedAt = DateTime.UtcNow;
        ManagerNotes = managerNotes;
        MarkAsUpdated();

        return Result.Success;
    }

    public ErrorOr<Success> Reject(Guid processedById, string managerNotes)
    {
        if (Status != ResignationRequestStatus.Pending)
            return ResignationRequestErrors.NotPending;

        if (string.IsNullOrWhiteSpace(managerNotes))
            return ResignationRequestErrors.RejectReasonRequired;

        Status = ResignationRequestStatus.Rejected;
        ProcessedById = processedById;
        ProcessedAt = DateTime.UtcNow;
        ManagerNotes = managerNotes;
        MarkAsUpdated();

        return Result.Success;
    }
}