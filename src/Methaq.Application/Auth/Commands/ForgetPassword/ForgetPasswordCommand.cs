using ErrorOr;
using MediatR;

namespace Methaq.Application.Auth.Commands.ForgetPassword;

public record ForgotPasswordCommand(string Email) : IRequest<ErrorOr<Success>>;