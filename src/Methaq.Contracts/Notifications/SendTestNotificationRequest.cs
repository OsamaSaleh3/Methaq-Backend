namespace Methaq.Contracts.Notifications;

public record SendTestNotificationRequest(
    string UserId,
    string Title,
    string Content,
    int Type,
    Guid? RelatedEntityId
);
