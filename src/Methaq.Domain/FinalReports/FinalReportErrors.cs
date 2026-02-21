using ErrorOr;

namespace Methaq.Domain.FinalReports;

public static class FinalReportErrors
{
    public static readonly Error SectionIdRequired = Error.Validation(
        code: "FinalReport.SectionId",
        description: "Section ID is required.");

    public static readonly Error StudentReportNull = Error.Validation(
        code: "FinalReport.StudentReportNull",
        description: "Student report cannot be null.");

    public static readonly Error StudentReportAlreadyExists = Error.Conflict(
        code: "FinalReport.StudentReportExists",
        description: "Final report for this student already exists.");

    public static readonly Error EmailAlreadySent = Error.Conflict(
        code: "FinalReport.EmailAlreadySent",
        description: "Emails have already been sent for this report.");
}
