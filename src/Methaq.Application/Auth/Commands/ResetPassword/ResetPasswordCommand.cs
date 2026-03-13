using ErrorOr;
using MediatR;

namespace Methaq.Application.Auth.Commands.ResetPassword;

public record ResetPasswordCommand(
    string Email,
    string Otp,
    string NewPassword) : IRequest<ErrorOr<Success>>;