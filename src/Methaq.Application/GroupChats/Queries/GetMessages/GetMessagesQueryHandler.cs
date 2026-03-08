using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.GroupChats.Queries.GetMessages;

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, ErrorOr<List<MessageResponse>>>
{
    private readonly IGroupChatRepository _groupChatRepository;

    public GetMessagesQueryHandler(IGroupChatRepository groupChatRepository)
    {
        _groupChatRepository = groupChatRepository;
    }

    public async Task<ErrorOr<List<MessageResponse>>> Handle(GetMessagesQuery query, CancellationToken cancellationToken)
    {
        var chat = await _groupChatRepository.GetByIdAsync(query.GroupChatId);
        if (chat is null)
            return Error.NotFound("GroupChat.NotFound", "Group chat not found.");

        var messages = await _groupChatRepository.GetMessagesByGroupChatIdAsync(query.GroupChatId);

        return messages.Select(m => new MessageResponse(
            m.Id,
            m.GroupChatId,
            m.SenderId,
            m.Sender.FullName,
            m.Content,
            m.AttachmentUrl,
            m.EditedAt is not null,
            m.IsDeleted,
            m.CreatedAt
        )).ToList();
    }
}