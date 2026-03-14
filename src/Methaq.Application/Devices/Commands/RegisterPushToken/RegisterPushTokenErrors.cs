using ErrorOr;

namespace Methaq.Application.Devices.Commands.RegisterPushToken;

public static class RegisterPushTokenErrors
{
    public static readonly Error InvalidToken = Error.Validation(
        code: "PushToken.Invalid",
        description: "Invalid push token.");
}