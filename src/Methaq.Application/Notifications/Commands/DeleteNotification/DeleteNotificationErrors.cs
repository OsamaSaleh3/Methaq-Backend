using ErrorOr;

namespace Methaq.Application.UseCases.Notifications.Commands.DeleteNotification;

public static class DeleteNotificationErrors
{
    public static readonly Error NotFound = Error.NotFound(
        code: "Notification.NotFound",
        description: "Notification not found.");

    public static readonly Error Forbidden = Error.Forbidden(
        code: "Notification.Forbidden",
        description: "You are not allowed to delete this notification.");
}