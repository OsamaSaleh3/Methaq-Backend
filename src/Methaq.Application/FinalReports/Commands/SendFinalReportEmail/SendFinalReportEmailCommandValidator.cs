using FluentValidation;

namespace Methaq.Application.FinalReports.Commands.SendFinalReportEmail;

public class SendFinalReportEmailCommandValidator : AbstractValidator<SendFinalReportEmailCommand>
{
    public SendFinalReportEmailCommandValidator()
    {
        RuleFor(x => x.FinalReportId)
            .NotEmpty().WithMessage("Final Report ID is required.");
    }
}