using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;
using Methaq.Domain.Notifications.enums;

namespace Methaq.Domain.Notifications;

public class Notification : BaseEntity
{
    public string UserId { get; private set; } = null!;
    public ApplicationUser User { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public NotificationType Type { get; private set; }
    public bool IsRead { get; private set; }
    public Guid? RelatedEntityId { get; private set; }

    protected Notification() { }

    private Notification(string userId, string title, string content, NotificationType type, Guid? relatedEntityId)
    {
        UserId = userId;
        Title = title;
        Content = content;
        Type = type;
        IsRead = false;
        RelatedEntityId = relatedEntityId;
    }

    public static ErrorOr<Notification> Create(string userId, string title, string content, NotificationType type, Guid? relatedEntityId = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return NotificationErrors.UserIdRequired;

        if (string.IsNullOrWhiteSpace(title))
            return NotificationErrors.TitleRequired;

        if (string.IsNullOrWhiteSpace(content))
            return NotificationErrors.ContentRequired;

        return new Notification(userId, title, content, type, relatedEntityId);
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
