using Methaq.Domain.Notifications;

namespace Methaq.Application.Common.Interfaces;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(Guid id);
    Task<List<Notification>> GetByUserIdAsync(string userId);
    Task<List<Notification>> GetUnreadByUserIdAsync(string userId);
    Task<int> GetUnreadCountAsync(string userId);
    Task AddAsync(Notification notification);
    void Delete(Notification notification);
}