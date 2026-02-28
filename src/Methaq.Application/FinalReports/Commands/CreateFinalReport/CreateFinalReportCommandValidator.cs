using FluentValidation;

namespace Methaq.Application.FinalReports.Commands.CreateFinalReport;

public class CreateFinalReportCommandValidator : AbstractValidator<CreateFinalReportCommand>
{
    public CreateFinalReportCommandValidator()
    {
        RuleFor(x => x.SectionId)
            .NotEmpty().WithMessage("Section ID is required.");

        RuleFor(x => x.GeneralNotes)
            .MaximumLength(1000).WithMessage("General notes must not exceed 1000 characters.")
            .When(x => x.GeneralNotes is not null);
    }
}