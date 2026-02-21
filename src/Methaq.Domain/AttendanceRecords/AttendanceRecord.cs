using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.AttendanceRecords.enums;
using Methaq.Domain.Lectures;
using Methaq.Domain.Students;

namespace Methaq.Domain.AttendanceRecords;

public class AttendanceRecord : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;

    public Guid LectureId { get; private set; }
    public Lecture Lecture { get; private set; } = null!;

    public AttendanceStatus Status { get; private set; }
    public string? ExcuseReason { get; private set; }  
    public string? Notes { get; private set; }

    protected AttendanceRecord() { }

    private AttendanceRecord(Guid studentId, Guid lectureId, AttendanceStatus status, string? excuseReason, string? notes)
    {
        StudentId = studentId;
        LectureId = lectureId;
        Status = status;
        ExcuseReason = excuseReason;
        Notes = notes;
    }

    public static ErrorOr<AttendanceRecord> Create(Guid studentId, Guid lectureId, AttendanceStatus status, string? excuseReason = null, string? notes = null)
    {
        if (studentId == Guid.Empty)
            return AttendanceRecordErrors.StudentIdRequired;

        if (lectureId == Guid.Empty)
            return AttendanceRecordErrors.LectureIdRequired;

        if (status == AttendanceStatus.Excused && string.IsNullOrWhiteSpace(excuseReason))
            return AttendanceRecordErrors.ExcuseReasonRequired;

        return new AttendanceRecord(studentId, lectureId, status, excuseReason, notes);
    }

    public ErrorOr<Success> UpdateStatus(AttendanceStatus status, string? excuseReason = null, string? notes = null)
    {
        if (status == AttendanceStatus.Excused && string.IsNullOrWhiteSpace(excuseReason))
            return AttendanceRecordErrors.ExcuseReasonRequired;

        Status = status;
        ExcuseReason = excuseReason;
        Notes = notes;
        MarkAsUpdated();
        return Result.Success;
    }
}
