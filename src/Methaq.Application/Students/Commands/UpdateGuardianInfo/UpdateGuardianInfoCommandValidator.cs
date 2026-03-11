using FluentValidation;

namespace Methaq.Application.UseCases.Students.Commands.UpdateGuardianInfo;

public class UpdateGuardianInfoCommandValidator : AbstractValidator<UpdateGuardianInfoCommand>
{
    public UpdateGuardianInfoCommandValidator()
    {
        RuleFor(x => x.GuardianPhone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^07[789]\d{7}$").WithMessage("Invalid Jordanian phone number.");

        RuleFor(x => x.GuardianEmail)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.GuardianEmail))
            .WithMessage("Invalid guardian email format.");
    }
}