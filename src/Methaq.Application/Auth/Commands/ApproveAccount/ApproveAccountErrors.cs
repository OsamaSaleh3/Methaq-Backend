using ErrorOr;

namespace Methaq.Application.Auth.Commands.ApproveAccount;

public static class ApproveAccountErrors
{
    public static readonly Error UserNotFound = Error.NotFound(
        code: "Auth.UserNotFound",
        description: "User not found.");

    public static readonly Error AlreadyApproved = Error.Conflict(
        code: "Auth.AlreadyApproved",
        description: "Account is already approved.");

    public static readonly Error EmailNotConfirmed = Error.Conflict(
        code: "Auth.EmailNotConfirmed",
        description: "User email is not confirmed yet.");
}