using ErrorOr;
using MediatR;

namespace Methaq.Application.GroupChats.Queries.GetMyChats;

public record GetMyChatsQuery(string UserId) : IRequest<ErrorOr<List<SupervisorChatResponse>>>;

public record SupervisorChatResponse(
    Guid Id,
    string Name,
    Guid SectionId,
    int MembersCount,
    int UnreadCount,
    LastMessageInfo? LastMessage);

public record LastMessageInfo(
    Guid MessageId,
    string SenderName,
    string Content,
    DateTime SentAt);