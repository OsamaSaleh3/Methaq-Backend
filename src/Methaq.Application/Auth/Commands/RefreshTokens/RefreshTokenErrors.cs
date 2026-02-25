using ErrorOr;

namespace Methaq.Application.Auth.Commands.RefreshTokens;

public static class RefreshTokenErrors
{
    public static readonly Error InvalidRefreshToken = Error.Unauthorized(
        code: "Auth.InvalidRefreshToken",
        description: "Refresh token is invalid or expired.");
}