using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;

namespace Methaq.Domain.GroupChats;

public class UserChatLastRead
{
    public string UserId { get; private set; } = null!;
    public ApplicationUser User { get; private set; } = null!;

    public Guid GroupChatId { get; private set; }
    public GroupChat GroupChat { get; private set; } = null!;

    public Guid? LastReadMessageId { get; private set; }
    public DateTime LastReadAt { get; private set; }

    protected UserChatLastRead() { }

    private UserChatLastRead(string userId, Guid groupChatId, Guid? lastReadMessageId)
    {
        UserId = userId;
        GroupChatId = groupChatId;
        LastReadMessageId = lastReadMessageId;
        LastReadAt = DateTime.UtcNow;
    }

    public static UserChatLastRead Create(string userId, Guid groupChatId, Guid? lastReadMessageId = null)
    {
        return new UserChatLastRead(userId, groupChatId, lastReadMessageId);
    }

    public void UpdateLastRead(Guid lastReadMessageId)
    {
        LastReadMessageId = lastReadMessageId;
        LastReadAt = DateTime.UtcNow;
    }
}