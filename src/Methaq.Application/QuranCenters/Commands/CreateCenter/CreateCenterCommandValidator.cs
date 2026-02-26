using FluentValidation;

namespace Methaq.Application.QuranCenters.Commands.CreateCenter;

public class CreateCenterCommandValidator:AbstractValidator<CreateCenterCommand>
{
    public CreateCenterCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Center name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500);

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(200);

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^07[789]\d{7}$").WithMessage("Invalid phone number.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.ManagerId)
            .NotEmpty().WithMessage("Manager ID is required.");
    }
}
