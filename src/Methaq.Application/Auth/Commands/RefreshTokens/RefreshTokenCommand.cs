using ErrorOr;
using MediatR;
using Methaq.Application.Auth.Commands.RefreshTokens.Responses;

namespace Methaq.Application.Auth.Commands.RefreshTokens;

public record RefreshTokenCommand(
    string RefreshToken
) : IRequest<ErrorOr<RefreshTokenResponse>>;

