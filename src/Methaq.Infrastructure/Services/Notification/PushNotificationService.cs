using FirebaseAdmin.Messaging;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Notifications;
using Microsoft.Extensions.Logging;

namespace Methaq.Infrastructure.Services;

public class PushNotificationService : IPushNotificationService
{
    private readonly IPushTokenRepository _pushTokenRepository;
    private readonly ILogger<PushNotificationService> _logger;

    public PushNotificationService(
        IPushTokenRepository pushTokenRepository,
        ILogger<PushNotificationService> logger)
    {
        _pushTokenRepository = pushTokenRepository;
        _logger = logger;
    }

    public async Task SendAsync(string userId, string title, string body, CancellationToken cancellationToken = default)
    {
        var pushToken = await _pushTokenRepository.GetByUserIdAsync(userId, cancellationToken);
        if (pushToken is null) return;

        try
        {
            var message = new Message
            {
                Token = pushToken.Token,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body
                }
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send push notification to user {UserId}", userId);
        }
    }

    public async Task SendToMultipleAsync(List<string> userIds, string title, string body, CancellationToken cancellationToken = default)
    {
        var tokens = await _pushTokenRepository.GetTokensByUserIdsAsync(userIds, cancellationToken);
        if (!tokens.Any()) return;

        try
        {
            var message = new MulticastMessage
            {
                Tokens = tokens,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body
                }
            };

            await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send multicast push notification");
        }
    }
}