using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.GroupChats.Commands.SendMessage;

public record SendMessageCommand(
    Guid GroupChatId,
    string SenderId,
    string Content,
    Stream? AttachmentStream,      
    string? AttachmentFileName,    
    string? AttachmentContentType
) : IRequest<ErrorOr<MessageDto>>;