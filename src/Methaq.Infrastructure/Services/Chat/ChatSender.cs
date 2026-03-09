using Methaq.Application.Common.Interfaces;
using Methaq.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure.Services.Chat
{
    public class ChatSender : IChatSender
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatSender(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(Guid GroupChatId, MessageDto message)
        {
            await _hubContext.Clients
                .Group(GroupChatId.ToString())
                .SendAsync("ReceiveMessage", message);
        }
        public async Task EditMessageAsync(Guid groupChatId, Guid messageId, string newContent)
        {
            await _hubContext.Clients
           .Group(groupChatId.ToString())
           .SendAsync("MessageEdited", messageId, newContent);
        }
        public async Task DeleteMessageAsync(Guid groupChatId, Guid messageId)
        {
            await _hubContext.Clients
           .Group(groupChatId.ToString())
           .SendAsync("MessageDeleted", messageId);
        }
        
    }
}
