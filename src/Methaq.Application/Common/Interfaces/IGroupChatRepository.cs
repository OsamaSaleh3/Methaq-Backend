using Methaq.Domain.GroupChats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Common.Interfaces
{
    public interface IGroupChatRepository
    {
        Task<GroupChat?> GetBySectionIdAsync(Guid sectionId);
        Task AddAsync(GroupChat groupChat, CancellationToken cancellationToken);
    }
}
