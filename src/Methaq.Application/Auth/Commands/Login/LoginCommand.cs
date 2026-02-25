using ErrorOr;
using MediatR;
using Methaq.Application.Auth.Commands.Login.Responses;

namespace Methaq.Application.Auth.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<ErrorOr<LoginResponse>>;