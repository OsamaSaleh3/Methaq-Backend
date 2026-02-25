using ErrorOr;

namespace Methaq.Application.Auth.Commands.ConfirmEmail;

public static class ConfirmEmailErrors
{
    public static readonly Error UserNotFound = Error.NotFound(
        code: "Auth.UserNotFound",
        description: "User not found.");

    public static readonly Error InvalidOtp = Error.Validation(
        code: "Auth.InvalidOtp",
        description: "OTP is invalid or expired.");

    public static readonly Error EmailAlreadyConfirmed = Error.Conflict(
        code: "Auth.EmailAlreadyConfirmed",
        description: "Email is already confirmed.");

    public static readonly Error ConfirmFailed = Error.Failure(
        code: "Auth.ConfirmFailed",
        description: "Failed to confirm email. Please try again.");
}