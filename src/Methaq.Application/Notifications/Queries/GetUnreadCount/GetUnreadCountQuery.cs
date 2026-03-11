using ErrorOr;
using MediatR;

namespace Methaq.Application.UseCases.Notifications.Queries.GetUnreadCount;

public record GetUnreadCountQuery(string UserId) : IRequest<ErrorOr<int>>;