using Methaq.Domain.PushTokens;
using Methaq.Application.Common.Interfaces;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Methaq.Infrastructure.Repositories;

public class PushTokenRepository : IPushTokenRepository
{
    private readonly ApplicationDbContext _context;

    public PushTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PushToken?> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.PushTokens
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
    public async Task AddAsync(PushToken pushToken, CancellationToken cancellationToken)
    {
        await _context.PushTokens.AddAsync(pushToken, cancellationToken);
    }
    public async Task<List<string>> GetTokensByUserIdsAsync(List<string> userIds, CancellationToken cancellationToken)
    {
        return await _context.PushTokens
            .Where(x => userIds.Contains(x.UserId))
            .Select(x => x.Token)
            .ToListAsync(cancellationToken);
    }
}
