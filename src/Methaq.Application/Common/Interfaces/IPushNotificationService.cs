namespace Methaq.Application.Common.Interfaces;

public interface IPushNotificationService
{
    Task SendAsync(string userId, string title, string body, CancellationToken cancellationToken = default);
    Task SendToMultipleAsync(List<string> userIds, string title, string body, CancellationToken cancellationToken = default);
}