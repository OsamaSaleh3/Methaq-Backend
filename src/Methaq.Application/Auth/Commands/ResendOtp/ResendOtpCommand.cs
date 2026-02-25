using ErrorOr;
using MediatR;

namespace Methaq.Application.Auth.Commands.ResendOtp;

public record ResendOtpCommand(
    string UserId
) : IRequest<ErrorOr<Success>>;