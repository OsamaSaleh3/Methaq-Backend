using Methaq.Application.Common.Interfaces;
using Methaq.Domain.GroupChats;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Methaq.Infrastructure.Repositories
{
    public class GroupChatRepository : IGroupChatRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(GroupChat groupChat, CancellationToken cancellationToken)
        {
            await _context.GroupChats.AddAsync(groupChat, cancellationToken);
        }

        public async Task<GroupChat?> GetBySectionIdAsync(Guid sectionId)
        {
            return await _context.GroupChats.FirstOrDefaultAsync(gc => gc.SectionId == sectionId);
        }
    }
}
