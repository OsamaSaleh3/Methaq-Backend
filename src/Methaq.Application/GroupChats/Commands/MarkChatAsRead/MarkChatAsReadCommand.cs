using ErrorOr;
using MediatR;

namespace Methaq.Application.GroupChats.Commands.MarkChatAsRead;

public record MarkChatAsReadCommand(
    string UserId,
    Guid GroupChatId,
    Guid LastMessageId) : IRequest<ErrorOr<Success>>;