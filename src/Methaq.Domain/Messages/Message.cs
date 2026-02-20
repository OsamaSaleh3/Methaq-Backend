using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;
using System;

namespace Methaq.Domain.Messages;

public class Message : BaseEntity
{
    public Guid ConversationId { get; private set; }
    public string SenderId { get; private set; } = null!;
    public ApplicationUser Sender { get; private set; } = null!;

    public string Content { get; private set; } = null!;
    public string? AttachmentUrl { get; private set; }

    public bool IsRead { get; private set; }
    public DateTime? ReadAt { get; private set; }

    public bool IsDeleted { get; private set; }
    public DateTime? EditedAt { get; private set; }

    protected Message() { }

    private Message(Guid conversationId, string senderId, string content, string? attachmentUrl)
    {
        ConversationId = conversationId;
        SenderId = senderId;
        Content = content;
        AttachmentUrl = attachmentUrl;
        IsRead = false;
        IsDeleted = false;
    }

    public static ErrorOr<Message> Create(Guid conversationId, string senderId, string content, string? attachmentUrl = null)
    {
        if (conversationId == Guid.Empty)
            return MessageErrors.ConversationIdRequired;

        if (string.IsNullOrWhiteSpace(senderId))
            return MessageErrors.SenderIdRequired;

        if (string.IsNullOrWhiteSpace(content))
            return MessageErrors.ContentRequired;

        return new Message(conversationId, senderId, content, attachmentUrl);
    }

    public ErrorOr<Success> MarkAsRead()
    {
        if (IsRead)
            return MessageErrors.AlreadyRead;

        IsRead = true;
        ReadAt = DateTime.UtcNow;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> EditContent(string newContent, string requesterId)
    {
        if (IsDeleted)
            return MessageErrors.CannotEditDeletedMessage;

        if (SenderId != requesterId)
            return MessageErrors.CannotEditOthersMessage;

        if (string.IsNullOrWhiteSpace(newContent))
            return MessageErrors.ContentRequired;

        Content = newContent;
        EditedAt = DateTime.UtcNow;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Delete(string requesterId)
    {
        if (IsDeleted)
            return MessageErrors.AlreadyDeleted;

        if (SenderId != requesterId)
            return MessageErrors.CannotDeleteOthersMessage;

        IsDeleted = true;
        Content = string.Empty;
        MarkAsUpdated();
        return Result.Success;
    }
}