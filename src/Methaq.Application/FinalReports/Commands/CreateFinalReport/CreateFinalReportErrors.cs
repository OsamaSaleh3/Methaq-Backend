using ErrorOr;

namespace Methaq.Application.FinalReports.Commands.CreateFinalReport;

public static class CreateFinalReportErrors
{
    public static readonly Error SectionNotFound = Error.NotFound(
        code: "FinalReport.SectionNotFound",
        description: "Section not found.");

    public static readonly Error SectionClosed = Error.Conflict(
        code: "FinalReport.SectionClosed",
        description: "Cannot generate a report for a closed section.");

    public static readonly Error NoStudentsInSection = Error.Conflict(
        code: "FinalReport.NoStudents",
        description: "Section has no students to generate a report for.");

    public static readonly Error ReportAlreadyExists = Error.Conflict(
        code: "FinalReport.AlreadyExists",
        description: "A final report already exists for this section.");
}