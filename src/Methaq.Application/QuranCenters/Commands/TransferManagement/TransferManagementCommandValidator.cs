using FluentValidation;

namespace Methaq.Application.QuranCenters.Commands.TransferManagement;

public class TransferManagementCommandValidator : AbstractValidator<TransferManagementCommand>
{
    public TransferManagementCommandValidator()
    {
        RuleFor(x => x.CenterId)
            .NotEmpty().WithMessage("Center ID is required.");

        RuleFor(x => x.NewManagerId)
            .NotEmpty().WithMessage("New manager ID is required.");
    }
}