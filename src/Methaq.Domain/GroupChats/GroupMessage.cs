using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;

namespace Methaq.Domain.GroupChats;

public class GroupMessage : BaseEntity
{
    public Guid GroupChatId { get; private set; }
    public string SenderId { get; private set; } = null!;
    public ApplicationUser Sender { get; private set; } = null!;

    public string Content { get; private set; } = null!;
    public string? AttachmentUrl { get; private set; }

    public bool IsDeleted { get; private set; }
    public DateTime? EditedAt { get; private set; }

    protected GroupMessage() { }

    private GroupMessage(Guid groupChatId, string senderId, string content, string? attachmentUrl)
    {
        GroupChatId = groupChatId;
        SenderId = senderId;
        Content = content;
        AttachmentUrl = attachmentUrl;
        IsDeleted = false;
    }

    public static ErrorOr<GroupMessage> Create(Guid groupChatId, string senderId, string content, string? attachmentUrl = null)
    {
        if (groupChatId == Guid.Empty)
            return GroupChatErrors.ChatIdRequired;

        if (string.IsNullOrWhiteSpace(senderId))
            return GroupChatErrors.SenderIdRequired;

        if (string.IsNullOrWhiteSpace(content))
            return GroupChatErrors.ContentRequired;

        return new GroupMessage(groupChatId, senderId, content, attachmentUrl);
    }

    public ErrorOr<Success> Edit(string newContent, string requesterId)
    {
        if (IsDeleted)
            return GroupChatErrors.CannotEditDeleted;

        if (SenderId != requesterId)
            return GroupChatErrors.CannotEditOthers;

        if (string.IsNullOrWhiteSpace(newContent))
            return GroupChatErrors.ContentRequired;

        Content = newContent;
        EditedAt = DateTime.UtcNow;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Delete(string requesterId)
    {
        if (IsDeleted)
            return GroupChatErrors.AlreadyDeleted;

        if (SenderId != requesterId)
            return GroupChatErrors.CannotDeleteOthers;

        IsDeleted = true;
        Content = string.Empty;
        MarkAsUpdated();
        return Result.Success;
    }
}
