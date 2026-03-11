using ErrorOr;

namespace Methaq.Application.UseCases.Notifications.Commands.MarkAllAsRead;

public static class MarkAllAsReadErrors
{
    public static readonly Error NoUnreadNotifications = Error.NotFound(
        code: "Notification.NoUnread",
        description: "No unread notifications found.");
}