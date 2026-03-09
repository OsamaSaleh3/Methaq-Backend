using Methaq.Domain.Notifications.enums;

namespace Methaq.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendAsync(string userId, string title, string content, NotificationType type, Guid? relatedEntityId = null);
}