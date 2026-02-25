using ErrorOr;

namespace Methaq.Application.Auth.Commands.ResendOtp;

public static class ResendOtpErrors
{
    public static readonly Error UserNotFound = Error.NotFound(
        code: "Auth.UserNotFound",
        description: "User not found.");

    public static readonly Error EmailAlreadyConfirmed = Error.Conflict(
        code: "Auth.EmailAlreadyConfirmed",
        description: "Email is already confirmed.");
}