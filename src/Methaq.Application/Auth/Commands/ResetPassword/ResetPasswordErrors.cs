using ErrorOr;

namespace Methaq.Application.Auth.Commands.ResetPassword;

public static class ResetPasswordErrors
{
    public static readonly Error InvalidOtp = Error.Validation(
        code: "ResetPassword.InvalidOtp",
        description: "Invalid or expired OTP.");

    public static readonly Error ResetFailed = Error.Failure(
        code: "ResetPassword.ResetFailed",
        description: "Failed to reset password.");
}