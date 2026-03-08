using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.GroupChats.Queries.GetChatBySection;

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

        return new GroupChatResponse(
            chat.Id,
            chat.Name,
            chat.SectionId,
            chat.Members.Select(m => new MemberResponse(m.Id, m.FullName)).ToList()
        );
    }
}