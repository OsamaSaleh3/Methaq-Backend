using ErrorOr;

namespace Methaq.Application.FinalReports.Commands.SendFinalReportEmail;

public static class SendFinalReportEmailErrors
{
    public static readonly Error ReportNotFound = Error.NotFound(
        code: "FinalReport.NotFound",
        description: "Final report not found.");

    public static readonly Error EmailAlreadySent = Error.Conflict(
        code: "FinalReport.EmailAlreadySent",
        description: "Emails have already been sent for this report.");
}