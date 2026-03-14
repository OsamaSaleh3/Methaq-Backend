using FluentValidation;

namespace Methaq.Application.Devices.Commands.RegisterPushToken;

public class RegisterPushTokenCommandValidator : AbstractValidator<RegisterPushTokenCommand>
{
    public RegisterPushTokenCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Push token is required.");

        RuleFor(x => x.Platform)
            .NotEmpty()
            .Must(p => p == "ios" || p == "android")
            .WithMessage("Platform must be 'ios' or 'android'.");
    }
}