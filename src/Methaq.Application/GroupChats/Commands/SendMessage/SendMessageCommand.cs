using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.GroupChats.Commands.SendMessage;

public record SendMessageCommand(
    Guid GroupChatId,
    string SenderId,
    string Content,
    string? AttachmentUrl
) : IRequest<ErrorOr<MessageDto>>;