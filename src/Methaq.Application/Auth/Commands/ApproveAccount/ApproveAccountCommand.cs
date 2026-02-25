using ErrorOr;
using MediatR;

namespace Methaq.Application.Auth.Commands.ApproveAccount;

public record ApproveAccountCommand(
    string UserId
) : IRequest<ErrorOr<Success>>;