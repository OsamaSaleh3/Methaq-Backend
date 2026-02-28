using ErrorOr;
using MediatR;

namespace Methaq.Application.FinalReports.Commands.SendFinalReportEmail;

public record SendFinalReportEmailCommand(Guid FinalReportId) : IRequest<ErrorOr<Success>>;