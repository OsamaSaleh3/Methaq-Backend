using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;
using Methaq.Domain.Notifications.enums;
using System;

namespace Methaq.Domain.Notifications;

public class Notification : BaseEntity
{
    public Guid UserId { get; private set; }
    public ApplicationUser User { get; private set; } = null!;

    public string Title { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public NotificationType Type { get; private set; }
    public bool IsRead { get; private set; }

    protected Notification() { }

    private Notification(Guid userId, string title, string content, NotificationType type)
    {
        UserId = userId;
        Title = title;
        Content = content;
        Type = type;
        IsRead = false;
    }

    public static ErrorOr<Notification> Create(Guid userId, string title, string content, NotificationType type)
    {
        if (userId == Guid.Empty)
            return NotificationErrors.UserIdRequired;

        if (string.IsNullOrWhiteSpace(title))
            return NotificationErrors.TitleRequired;

        if (string.IsNullOrWhiteSpace(content))
            return NotificationErrors.ContentRequired;

        return new Notification(userId, title, content, type);
    }

    public ErrorOr<Success> MarkAsRead()
    {
        if (IsRead)
            return NotificationErrors.AlreadyRead;

        IsRead = true;
        MarkAsUpdated();
        return Result.Success;
    }
}