using ErrorOr;
using MediatR;

namespace Methaq.Application.Auth.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<ErrorOr<LoginResponse>>;

public record LoginResponse(
   string UserId,
   string FullName,
   string Email,
   string Role,
   string AccessToken,
   string RefreshToken
);