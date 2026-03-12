using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.GroupChats;

namespace Methaq.Application.GroupChats.Queries.GetMyChats;

public class GetMyChatsQueryHandler : IRequestHandler<GetMyChatsQuery, ErrorOr<List<SupervisorChatResponse>>>
{
    private readonly IGroupChatRepository _groupChatRepository;

    public GetMyChatsQueryHandler(IGroupChatRepository groupChatRepository)
    {
        _groupChatRepository = groupChatRepository;
    }

    public async Task<ErrorOr<List<SupervisorChatResponse>>> Handle(GetMyChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await _groupChatRepository.GetBySupervisorIdAsync(request.UserId, cancellationToken);

        return chats.Select(chat =>
        {
            var lastRead = chat.LastReads.FirstOrDefault(lr => lr.UserId == request.UserId);

            int unreadCount = 0;
            if (lastRead?.LastReadMessageId is null)
            {
                unreadCount = chat.Messages.Count(m => !m.IsDeleted);
            }
            else
            {
                var lastReadMessage = chat.Messages.FirstOrDefault(m => m.Id == lastRead.LastReadMessageId);
                if (lastReadMessage is not null)
                    unreadCount = chat.Messages.Count(m => !m.IsDeleted && m.CreatedAt > lastReadMessage.CreatedAt);
            }

            var lastMessage = chat.Messages
                .Where(m => !m.IsDeleted)
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault();

            return new SupervisorChatResponse(
                chat.Id,
                chat.Name,
                chat.SectionId,
                chat.Members.Count,
                unreadCount,
                lastMessage is null ? null : new LastMessageInfo(
                    lastMessage.Id,
                    lastMessage.Sender.FullName,
                    lastMessage.Content,
                    lastMessage.CreatedAt));
        }).ToList();
    }
}