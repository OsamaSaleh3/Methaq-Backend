using ErrorOr;

namespace Methaq.Application.UseCases.Notifications.Commands.MarkAsRead;

public static class MarkAsReadErrors
{
    public static readonly Error NotFound = Error.NotFound(
        code: "Notification.NotFound",
        description: "Notification not found.");

    public static readonly Error Forbidden = Error.Forbidden(
        code: "Notification.Forbidden",
        description: "You are not allowed to mark this notification as read.");
}