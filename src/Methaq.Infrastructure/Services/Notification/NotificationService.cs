using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Notifications;
using Methaq.Domain.Notifications.enums;
using static Methaq.Application.Common.Interfaces.INotificationSender;

namespace Methaq.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationSender _notificationSender;
    private readonly IPushNotificationService _pushNotificationService;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(INotificationRepository notificationRepository, INotificationSender notificationSender, IPushNotificationService pushNotificationService, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _notificationSender = notificationSender;
        _pushNotificationService = pushNotificationService;
        _unitOfWork = unitOfWork;
    }

    public async Task SendAsync(string userId, string title, string content, NotificationType type, Guid? relatedEntityId = null)
    {
        var notificationResult = Notification.Create(userId, title, content, type, relatedEntityId);
        if (notificationResult.IsError)
            return;

        var notification = notificationResult.Value;
        await _notificationRepository.AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();

        await _notificationSender.SendNotificationAsync(userId, new NotificationResponse(
            notification.Id,
            notification.Title,
            notification.Content,
            (int)notification.Type,
            notification.IsRead,
            notification.RelatedEntityId,
            notification.CreatedAt));

        await _pushNotificationService.SendAsync(userId, title, content);
    }
}