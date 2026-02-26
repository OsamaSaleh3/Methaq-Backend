using FluentValidation;

namespace Methaq.Application.QuranCenters.Commands.UpdateCenterInfo;

public class UpdateCenterInfoCommandValidator : AbstractValidator<UpdateCenterInfoCommand>
{
    public UpdateCenterInfoCommandValidator()
    {
        RuleFor(x => x.CenterId)
            .NotEmpty().WithMessage("Center ID is required.");

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Location)
            .MaximumLength(200)
            .When(x => !string.IsNullOrWhiteSpace(x.Location));

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^07[789]\d{7}$").WithMessage("Invalid phone number.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}