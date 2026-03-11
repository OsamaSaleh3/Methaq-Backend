namespace Methaq.Application.Common.Interfaces;

public interface INotificationSender
{
    Task SendNotificationAsync(string userId, NotificationResponse notification);

    public record NotificationResponse(
        Guid Id,
        string Title,
        string Content,
        int Type,
        bool IsRead,
        Guid? RelatedEntityId,
        DateTime CreatedAt
    );
}