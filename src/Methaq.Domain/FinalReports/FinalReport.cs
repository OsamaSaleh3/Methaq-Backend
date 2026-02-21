using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.Sections;

namespace Methaq.Domain.FinalReports;

public class FinalReport : BaseEntity
{
    public Guid SectionId { get; private set; }
    public Section Section { get; private set; } = null!;

    public DateTime GeneratedAt { get; private set; }
    public string? GeneralNotes { get; private set; }

    private readonly List<StudentFinalReport> _studentReports = [];
    public IReadOnlyCollection<StudentFinalReport> StudentReports => _studentReports.AsReadOnly();
    public bool EmailSentToStudents { get; private set; }
    public DateTime? EmailSentAt { get; private set; }

    protected FinalReport() { }

    private FinalReport(Guid sectionId, string? generalNotes)
    {
        SectionId = sectionId;
        GeneralNotes = generalNotes;
        GeneratedAt = DateTime.UtcNow;
        EmailSentToStudents = false;
    }

    public static ErrorOr<FinalReport> Create(Guid sectionId, string? generalNotes = null)
    {
        if (sectionId == Guid.Empty)
            return FinalReportErrors.SectionIdRequired;

        return new FinalReport(sectionId, generalNotes);
    }

    public ErrorOr<Success> AddStudentReport(StudentFinalReport studentReport)
    {
        if (studentReport == null)
            return FinalReportErrors.StudentReportNull;

        if (_studentReports.Any(r => r.StudentId == studentReport.StudentId))
            return FinalReportErrors.StudentReportAlreadyExists;

        _studentReports.Add(studentReport);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> MarkEmailSent()
    {
        if (EmailSentToStudents)
            return FinalReportErrors.EmailAlreadySent;

        EmailSentToStudents = true;
        EmailSentAt = DateTime.UtcNow;
        MarkAsUpdated();
        return Result.Success;
    }
}

