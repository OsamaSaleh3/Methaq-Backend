using ErrorOr;

namespace Methaq.Application.Notifications.Commands.SendTestNotification;

public static class SendTestNotificationErrors
{
    public static readonly Error InvalidType = Error.Validation(
        code: "Notification.InvalidType",
        description: "Invalid notification type.");
}
