using ErrorOr;

namespace Methaq.Application.Auth.Commands.ForgetPassword;

public static class ForgotPasswordErrors
{
    public static readonly Error SendFailed = Error.Failure(
        code: "ForgotPassword.SendFailed",
        description: "Failed to send reset code.");
}