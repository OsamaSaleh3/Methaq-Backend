using Microsoft.AspNetCore.SignalR;

namespace Methaq.Infrastructure.Hubs;

public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    public async Task JoinChat(string groupChatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupChatId);
    }

    public async Task LeaveChat(string groupChatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupChatId);
    }
}