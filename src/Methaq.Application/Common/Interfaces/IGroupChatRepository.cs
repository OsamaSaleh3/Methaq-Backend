using Methaq.Domain.GroupChats;

namespace Methaq.Application.Common.Interfaces;

public interface IGroupChatRepository
{
    Task<GroupChat?> GetByIdAsync(Guid id);
    Task<GroupChat?> GetByIdWithMembersAsync(Guid id);
    Task<GroupChat?> GetBySectionIdAsync(Guid sectionId);
    Task<GroupMessage?> GetMessageByIdAsync(Guid messageId);
    Task<List<GroupMessage>> GetMessagesByGroupChatIdAsync(Guid groupChatId);
    Task AddAsync(GroupChat groupChat);
    Task<List<GroupChat>> GetBySupervisorIdAsync(string userId, CancellationToken cancellationToken);
    Task<UserChatLastRead?> GetLastReadAsync(string userId, Guid groupChatId, CancellationToken cancellationToken);
    Task AddLastReadAsync(UserChatLastRead lastRead, CancellationToken cancellationToken);
}