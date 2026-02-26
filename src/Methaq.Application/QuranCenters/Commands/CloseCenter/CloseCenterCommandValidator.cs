using FluentValidation;

namespace Methaq.Application.QuranCenters.Commands.CloseCenter;

public class CloseCenterCommandValidator : AbstractValidator<CloseCenterCommand>
{
    public CloseCenterCommandValidator()
    {
        RuleFor(x => x.CenterId)
            .NotEmpty().WithMessage("Center ID is required.");
    }
}