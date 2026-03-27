using FluentValidation;

namespace Methaq.Application.Auth.Commands.RegisterEmployee;

public class RegisterEmployeeCommandValidator : AbstractValidator<RegisterEmployeeCommand>
{
    public RegisterEmployeeCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50);

        RuleFor(x => x.SecondName)
            .NotEmpty().WithMessage("Second name is required.")
            .MaximumLength(50);

        RuleFor(x => x.ThirdName)
            .NotEmpty().WithMessage("Third name is required.")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^07[789]\d{7}$").WithMessage("Invalid Jordanian phone number.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(dob => dob <= DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18)))
            .WithMessage("Employee must be older than 18 years.");

        RuleFor(x => x.Degree)
            .IsInEnum().WithMessage("Invalid academic degree.");

        RuleFor(x => x.Specialization)
            .NotEmpty().WithMessage("Specialization is required.")
            .MaximumLength(100);

        RuleFor(x => x.IslamicQualifications)
            .MaximumLength(200)
            .When(x => !string.IsNullOrWhiteSpace(x.IslamicQualifications));

        RuleFor(x => x.CurrentJob)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentJob));

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid employee role.");
    }
}