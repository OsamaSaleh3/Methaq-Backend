using ErrorOr;

namespace Methaq.Application.FinalReports.Commands.AddStudentReport;

public static class AddStudentReportErrors
{
    public static readonly Error ReportNotFound = Error.NotFound(
        code: "FinalReport.NotFound",
        description: "Final report not found.");

    public static readonly Error StudentNotFound = Error.NotFound(
        code: "FinalReport.StudentNotFound",
        description: "Student not found.");

    public static readonly Error StudentNotInSection = Error.Conflict(
        code: "FinalReport.StudentNotInSection",
        description: "Student is not enrolled in this section.");
}