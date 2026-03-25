using ErrorOr;
using MediatR;
using Methaq.Domain.Notifications.enums;

namespace Methaq.Application.Notifications.Commands.SendTestNotification;

public record SendTestNotificationCommand(
    string UserId,
    string Title,
    string Content,
    NotificationType Type,
    Guid? RelatedEntityId
) : IRequest<ErrorOr<Success>>;
