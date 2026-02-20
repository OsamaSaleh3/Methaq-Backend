using ErrorOr;
using System;

namespace Methaq.Domain.Notifications;

public static class NotificationErrors
{
    public static readonly Error UserIdRequired = Error.Validation(
        code: "Notification.UserId",
        description: "User ID is required.");

    public static readonly Error TitleRequired = Error.Validation(
        code: "Notification.Title",
        description: "Notification title is required.");

    public static readonly Error ContentRequired = Error.Validation(
        code: "Notification.Content",
        description: "Notification content is required.");

    public static readonly Error AlreadyRead = Error.Conflict(
        code: "Notification.AlreadyRead",
        description: "Notification is already read.");
}
