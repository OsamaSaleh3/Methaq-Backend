using ErrorOr;
using MediatR;

namespace Methaq.Application.Devices.Commands.RegisterPushToken;

public record RegisterPushTokenCommand(
    string UserId,
    string Token,
    string Platform) : IRequest<ErrorOr<Success>>;