using ErrorOr;
using MediatR;

namespace Methaq.Application.GroupChats.Commands.DeleteMessage;

public record DeleteMessageCommand(
    Guid MessageId,
    string UserId
) : IRequest<ErrorOr<Success>>;