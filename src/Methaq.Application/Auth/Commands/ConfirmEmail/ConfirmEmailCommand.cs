using ErrorOr;
using MediatR;

namespace Methaq.Application.Auth.Commands.ConfirmEmail;

public record ConfirmEmailCommand(
    string UserId,
    string Otp
) : IRequest<ErrorOr<Success>>;