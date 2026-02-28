namespace Methaq.Contracts.FinalReports;

public record CreateFinalReportRequest(
    Guid SectionId,
    string? GeneralNotes
);