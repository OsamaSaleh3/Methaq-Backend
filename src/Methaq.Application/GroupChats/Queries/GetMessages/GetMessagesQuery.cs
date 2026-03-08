using ErrorOr;
using MediatR;

namespace Methaq.Application.GroupChats.Queries.GetMessages;

public record GetMessagesQuery(Guid GroupChatId) 
    : IRequest<ErrorOr<List<MessageResponse>>>;

public record MessageResponse(
    Guid Id,
    Guid GroupChatId,
    string SenderId,
    string SenderName,
    string Content,
    string? AttachmentUrl,
    bool IsEdited,
    bool IsDeleted,
    DateTime CreatedAt
);