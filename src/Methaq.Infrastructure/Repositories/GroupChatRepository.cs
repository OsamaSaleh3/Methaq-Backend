using Methaq.Application.Common.Interfaces;
using Methaq.Domain.GroupChats;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Methaq.Infrastructure.Repositories;

public class GroupChatRepository : IGroupChatRepository
{
    private readonly ApplicationDbContext _context;

    public GroupChatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GroupChat?> GetByIdAsync(Guid id)
    {
        return await _context.GroupChats
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<GroupChat?> GetByIdWithMembersAsync(Guid id)
    {
        return await _context.GroupChats
            .Include(g => g.Members)
            .Include(g => g.Messages)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<GroupChat?> GetBySectionIdAsync(Guid sectionId)
    {
        return await _context.GroupChats
            .Include(g => g.Members)
            .FirstOrDefaultAsync(g => g.SectionId == sectionId);
    }

    public async Task<GroupMessage?> GetMessageByIdAsync(Guid messageId)
    {
        return await _context.GroupMessages
            .FirstOrDefaultAsync(m => m.Id == messageId);
    }

    public async Task<List<GroupMessage>> GetMessagesByGroupChatIdAsync(Guid groupChatId)
    {
        return await _context.GroupMessages
            .Where(m => m.GroupChatId == groupChatId)
            .Include(m => m.Sender)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(GroupChat groupChat)
    {
        await _context.GroupChats.AddAsync(groupChat);
    }
}