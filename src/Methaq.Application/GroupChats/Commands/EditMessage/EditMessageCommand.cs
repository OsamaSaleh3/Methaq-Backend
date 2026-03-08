using ErrorOr;
using MediatR;

namespace Methaq.Application.GroupChats.Commands.EditMessage;

public record EditMessageCommand(
    Guid MessageId,
    string UserId,
    string NewContent
) : IRequest<ErrorOr<Success>>;