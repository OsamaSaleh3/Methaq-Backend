using ErrorOr;
using MediatR;

namespace Methaq.Application.FinalReports.Commands.CreateFinalReport;

public record CreateFinalReportCommand(
    Guid SectionId,
    string? GeneralNotes
) : IRequest<ErrorOr<Guid>>;