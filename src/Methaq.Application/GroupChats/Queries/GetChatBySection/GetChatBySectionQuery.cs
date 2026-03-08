using ErrorOr;
using MediatR;

namespace Methaq.Application.GroupChats.Queries.GetChatBySection;

public record GetChatBySectionQuery(Guid SectionId) 
    : IRequest<ErrorOr<GroupChatResponse>>;

public record GroupChatResponse(
    Guid Id,
    string Name,
    Guid SectionId,
    List<MemberResponse> Members
);

public record MemberResponse(
    string Id,
    string FullName
);