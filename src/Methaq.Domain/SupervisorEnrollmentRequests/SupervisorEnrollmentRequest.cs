using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.Employees;
using Methaq.Domain.QuranCenters;
using Methaq.Domain.SupervisorEnrollmentRequests.enums;

namespace Methaq.Domain.SupervisorEnrollmentRequests;

public class SupervisorEnrollmentRequest : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; } = null!;

    public Guid CenterId { get; private set; }
    public QuranCenter Center { get; private set; } = null!;

    public SupervisorRequestStatus Status { get; private set; }
    public string? RejectionReason { get; private set; }
    public DateTime? ReviewedAt { get; private set; }

    protected SupervisorEnrollmentRequest() { }

    private SupervisorEnrollmentRequest(Guid employeeId, Guid centerId)
    {
        EmployeeId = employeeId;
        CenterId = centerId;
        Status = SupervisorRequestStatus.Pending;
    }

    public static ErrorOr<SupervisorEnrollmentRequest> Create(Guid employeeId, Guid centerId)
    {
        if (employeeId == Guid.Empty)
            return SupervisorEnrollmentRequestErrors.EmployeeIdRequired;

        if (centerId == Guid.Empty)
            return SupervisorEnrollmentRequestErrors.CenterIdRequired;

        return new SupervisorEnrollmentRequest(employeeId, centerId);
    }

    public ErrorOr<Success> Approve()
    {
        if (Status != SupervisorRequestStatus.Pending)
            return SupervisorEnrollmentRequestErrors.AlreadyReviewed;

        Status = SupervisorRequestStatus.Approved;
        ReviewedAt = DateTime.UtcNow;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Reject(string? reason = null)
    {
        if (Status != SupervisorRequestStatus.Pending)
            return SupervisorEnrollmentRequestErrors.AlreadyReviewed;

        Status = SupervisorRequestStatus.Rejected;
        RejectionReason = reason;
        ReviewedAt = DateTime.UtcNow;
        MarkAsUpdated();
        return Result.Success;
    }
}