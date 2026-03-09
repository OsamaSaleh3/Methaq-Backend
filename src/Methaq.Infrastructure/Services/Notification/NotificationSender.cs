using Methaq.Application.Common.Interfaces;
using Methaq.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using static Methaq.Application.Common.Interfaces.INotificationSender;

namespace Methaq.Infrastructure.Services;

public class NotificationSender : INotificationSender
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationSender(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNotificationAsync(string userId, NotificationResponse notification)
    {
        await _hubContext.Clients
            .Group(userId)
            .SendAsync("ReceiveNotification", notification);
    }
}