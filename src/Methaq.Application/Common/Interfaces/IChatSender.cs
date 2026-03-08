using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Common.Interfaces
{
    public interface IChatSender
    {
        Task SendMessageAsync(Guid GroupChatId, MessageDto message);
        Task EditMessageAsync(Guid groupChatId, Guid messageId, string newContent);
        Task DeleteMessageAsync(Guid groupChatId, Guid messageId);
    }
    public record MessageDto(
    Guid Id,
    Guid GroupChatId,
    string SenderId,
    string SenderName,
    string Content,
    string? AttachmentUrl,
    DateTime CreatedAt
);
}
