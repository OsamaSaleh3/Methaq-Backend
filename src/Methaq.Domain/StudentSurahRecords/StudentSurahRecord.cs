using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.Students;
using Methaq.Domain.StudentSurahRecords;
using Methaq.Domain.StudentSurahRecords.enums;
namespace Methaq.Domain.StudentSurahRecords;

public class StudentSurahRecord : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;

    public string SurahName { get; private set; } = null!;
    public SurahStatus Status { get; private set; }
    public DateTime? CompletionDate { get; private set; }

    protected StudentSurahRecord() { }

    private StudentSurahRecord(Guid studentId, string surahName, SurahStatus status, DateTime? completionDate)
    {
        StudentId = studentId;
        SurahName = surahName;
        Status = status;
        CompletionDate = completionDate;
    }

    public static ErrorOr<StudentSurahRecord> Create(
        Guid studentId,
        string surahName,
        SurahStatus status = SurahStatus.Current,
        DateTime? completionDate = null)
    {
        if (studentId == Guid.Empty)
            return StudentSurahRecordErrors.StudentIdRequired;

        if (string.IsNullOrWhiteSpace(surahName))
            return StudentSurahRecordErrors.SurahNameRequired;

        if (status != SurahStatus.Current && completionDate == null)
            return StudentSurahRecordErrors.CompletionDateRequired;

        if (status == SurahStatus.Current && completionDate != null)
            return StudentSurahRecordErrors.CurrentStatusCannotHaveCompletionDate;

        if (completionDate.HasValue && completionDate.Value > DateTime.UtcNow)
            return StudentSurahRecordErrors.CompletionDateCannotBeInFuture;

        return new StudentSurahRecord(studentId, surahName, status, completionDate);
    }

    public ErrorOr<Success> MarkAsCompleted()
    {
        if (Status != SurahStatus.Current)
            return StudentSurahRecordErrors.AlreadyCompleted;

        Status = SurahStatus.CompletedThisSemester;
        CompletionDate = DateTime.UtcNow;
        MarkAsUpdated();
        return Result.Success;
    }
}