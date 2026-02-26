using ErrorOr;
using MediatR;

namespace Methaq.Application.Auth.Commands.RefreshTokens;

public record RefreshTokenCommand(
    string RefreshToken
) : IRequest<ErrorOr<RefreshTokenResponse>>;

public record RefreshTokenResponse(
   string AccessToken,
   string RefreshToken
);

