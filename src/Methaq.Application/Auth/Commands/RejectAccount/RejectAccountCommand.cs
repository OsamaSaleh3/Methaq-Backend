using ErrorOr;
using MediatR;

namespace Methaq.Application.Auth.Commands.RejectAccount;

public record RejectAccountCommand(
    string UserId,
    string? Reason
) : IRequest<ErrorOr<Success>>;