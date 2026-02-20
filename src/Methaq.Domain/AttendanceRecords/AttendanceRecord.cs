using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.Sections;
using Methaq.Domain.Students;
using System;

namespace Methaq.Domain.AttendanceRecords;

public class AttendanceRecord : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;

    public Guid SectionId { get; private set; }
    public Section Section { get; private set; } = null!;

    public DateTime Date { get; private set; }
    public bool IsPresent { get; private set; }
    public string? Notes { get; private set; }

    protected AttendanceRecord() { }

    private AttendanceRecord(Guid studentId, Guid sectionId, DateTime date, bool isPresent, string? notes)
    {
        StudentId = studentId;
        SectionId = sectionId;
        Date = date;
        IsPresent = isPresent;
        Notes = notes;
    }

    public static ErrorOr<AttendanceRecord> Create(Guid studentId, Guid sectionId, DateTime date, bool isPresent, string? notes = null)
    {
        if (studentId == Guid.Empty)
            return AttendanceRecordErrors.StudentIdRequired;

        if (sectionId == Guid.Empty)
            return AttendanceRecordErrors.SectionIdRequired;

        if (date > DateTime.UtcNow)
            return AttendanceRecordErrors.DateCannotBeInFuture;


        return new AttendanceRecord(studentId, sectionId, date, isPresent, notes);
    }

    public ErrorOr<Success> UpdateStatus(bool isPresent, string? notes = null)
    {
        IsPresent = isPresent;
        Notes = notes;
        MarkAsUpdated();
        return Result.Success;
    }
}