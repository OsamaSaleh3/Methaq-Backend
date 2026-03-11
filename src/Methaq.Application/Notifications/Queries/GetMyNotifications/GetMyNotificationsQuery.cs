using ErrorOr;
using MediatR;
using Methaq.Domain.Notifications.enums;

namespace Methaq.Application.UseCases.Notifications.Queries.GetMyNotifications;

public record GetMyNotificationsQuery(string UserId) : IRequest<ErrorOr<List<NotificationResponse>>>;

public record NotificationResponse(
    Guid Id,
    string Title,
    string Content,
    NotificationType Type,
    bool IsRead,
    Guid? RelatedEntityId,
    DateTime CreatedAt);