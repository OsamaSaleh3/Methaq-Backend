using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.GroupChats.Queries.GetChatBySection;

public class GetChatBySectionQueryHandler : IRequestHandler<GetChatBySectionQuery, ErrorOr<GroupChatResponse>>
{
    private readonly IGroupChatRepository _groupChatRepository;

    public GetChatBySectionQueryHandler(IGroupChatRepository groupChatRepository)
    {
        _groupChatRepository = groupChatRepository;
    }

    public async Task<ErrorOr<GroupChatResponse>> Handle(GetChatBySectionQuery query, CancellationToken cancellationToken)
    {
        var chat = await _groupChatRepository.GetBySectionIdAsync(query.SectionId);
        if (chat is null)
            return Error.NotFound("GroupChat.NotFound", "Group chat not found.");

        var lastRead = await _groupChatRepository.GetLastReadAsync(query.UserId, chat.Id, cancellationToken);

        int unreadCount = 0;
        var messages = await _groupChatRepository.GetMessagesByGroupChatIdAsync(chat.Id);

        if (lastRead?.LastReadMessageId is null)
        {
            unreadCount = messages.Count(m => !m.IsDeleted);
        }
        else
        {
            var lastReadMessage = messages.FirstOrDefault(m => m.Id == lastRead.LastReadMessageId);
            if (lastReadMessage is not null)
                unreadCount = messages.Count(m => !m.IsDeleted && m.CreatedAt > lastReadMessage.CreatedAt);
        }

        return new GroupChatResponse(
            chat.Id,
            chat.Name,
            chat.SectionId,
            chat.Members.Select(m => new MemberResponse(m.Id, m.FullName)).ToList(),
            unreadCount);
    }
}