using ErrorOr;
using MediatR;

namespace Methaq.Application.GroupChats.Queries.GetChatBySection;

public record GetChatBySectionQuery(Guid SectionId,string UserId) 
    : IRequest<ErrorOr<GroupChatResponse>>;

public record GroupChatResponse(
    Guid Id,
    string Name,
    Guid SectionId,
    List<MemberResponse> Members,
     int UnreadCount
);

public record MemberResponse(
    string Id,
    string FullName
);