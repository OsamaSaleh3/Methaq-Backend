using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.CenterEnrollmentRequests.enums;
using Methaq.Domain.QuranCenters;
using Methaq.Domain.Students;

namespace Methaq.Domain.CenterEnrollmentRequests;

public class CenterEnrollmentRequest : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;

    public Guid CenterId { get; private set; }
    public QuranCenter Center { get; private set; } = null!;

    public EnrollmentRequestStatus Status { get; private set; }
    public string? RejectionReason { get; private set; }
    public DateTime? ReviewedAt { get; private set; }

    protected CenterEnrollmentRequest() { }

    private CenterEnrollmentRequest(Guid studentId, Guid centerId)
    {
        StudentId = studentId;
        CenterId = centerId;
        Status = EnrollmentRequestStatus.Pending;
    }

    public static ErrorOr<CenterEnrollmentRequest> Create(Guid studentId, Guid centerId)
    {
        if (studentId == Guid.Empty)
            return CenterEnrollmentRequestErrors.StudentIdRequired;

        if (centerId == Guid.Empty)
            return CenterEnrollmentRequestErrors.CenterIdRequired;

        return new CenterEnrollmentRequest(studentId, centerId);
    }

    public ErrorOr<Success> Approve()
    {
        if (Status != EnrollmentRequestStatus.Pending)
            return CenterEnrollmentRequestErrors.AlreadyReviewed;

        Status = EnrollmentRequestStatus.Approved;
        ReviewedAt = DateTime.UtcNow;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Reject(string? reason = null)
    {
        if (Status != EnrollmentRequestStatus.Pending)
            return CenterEnrollmentRequestErrors.AlreadyReviewed;

        Status = EnrollmentRequestStatus.Rejected;
        RejectionReason = reason;
        ReviewedAt = DateTime.UtcNow;
        MarkAsUpdated();
        return Result.Success;
    }
}
