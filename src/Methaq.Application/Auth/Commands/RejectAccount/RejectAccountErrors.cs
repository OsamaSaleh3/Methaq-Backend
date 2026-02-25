using ErrorOr;

namespace Methaq.Application.Auth.Commands.RejectAccount;

public static class RejectAccountErrors
{
    public static readonly Error UserNotFound = Error.NotFound(
        code: "Auth.UserNotFound",
        description: "User not found.");

    public static readonly Error AlreadyRejected = Error.Conflict(
        code: "Auth.AlreadyRejected",
        description: "Account is already rejected.");

    public static readonly Error CannotRejectApproved = Error.Conflict(
        code: "Auth.CannotRejectApproved",
        description: "Cannot reject an already approved account.");
}