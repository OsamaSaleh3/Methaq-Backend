using FluentValidation;

namespace Methaq.Application.Auth.Commands.RegisterStudent;

public class RegisterStudentCommandValidator : AbstractValidator<RegisterStudentCommand>
{
    public RegisterStudentCommandValidator()
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
            .Must(date => date < DateTime.UtcNow.AddYears(-5))
            .WithMessage("Student must be older than 5 years.");

        RuleFor(x => x.GuardianName)
            .NotEmpty().WithMessage("Guardian name is required.")
            .MaximumLength(100);

        RuleFor(x => x.GuardianPhone)
            .NotEmpty().WithMessage("Guardian phone number is required.")
            .Matches(@"^07[789]\d{7}$").WithMessage("Invalid Jordanian guardian phone number.");

        RuleFor(x => x.GuardianEmail)
            .EmailAddress().WithMessage("Invalid guardian email address.")
            .When(x => !string.IsNullOrWhiteSpace(x.GuardianEmail));

        RuleFor(x => x.AcademicLevel)
            .NotEmpty().WithMessage("Academic level is required.");
    }
}