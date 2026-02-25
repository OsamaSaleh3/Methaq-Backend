using ErrorOr;

namespace Methaq.Application.Auth.Commands.Login;

public static class LoginErrors
{
    public static readonly Error InvalidCredentials = Error.Unauthorized(
        code: "Auth.InvalidCredentials",
        description: "Invalid email or password.");

    public static readonly Error EmailNotConfirmed = Error.Forbidden(
        code: "Auth.EmailNotConfirmed",
        description: "Please confirm your email before logging in.");

    public static readonly Error AccountPending = Error.Forbidden(
        code: "Auth.AccountPending",
        description: "Your account is pending approval.");

    public static readonly Error AccountRejected = Error.Forbidden(
        code: "Auth.AccountRejected",
        description: "Your account has been rejected.");
}