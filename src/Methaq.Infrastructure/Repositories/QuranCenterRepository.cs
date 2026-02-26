using Methaq.Application.Common.Interfaces;
using Methaq.Domain.QuranCenters;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Methaq.Infrastructure.Repositories;

public class QuranCenterRepository : IQuranCenterRepository
{
    private readonly ApplicationDbContext _context;

    public QuranCenterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(QuranCenter center, CancellationToken cancellationToken)
    {
        await _context.QuranCenters.AddAsync(center, cancellationToken);
    }

    public async Task<QuranCenter?> GetByIdAsync(Guid id)
    {
        return await _context.QuranCenters
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<QuranCenter?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.QuranCenters
            .Include(c => c.Manager)
                .ThenInclude(m => m.User)
            .Include(c => c.Supervisors)
                .ThenInclude(s => s.User)
            .Include(c => c.Sections)
                .ThenInclude(s => s.Supervisor)
                    .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<QuranCenter>> GetAllWithDetailsAsync()
    {
        return await _context.QuranCenters
            .Include(c => c.Manager)
                .ThenInclude(m => m.User)
            .Include(c => c.Sections)
            .ToListAsync();
    }

    public async Task<List<QuranCenter>> GetAllAsync()
    {
        return await _context.QuranCenters.ToListAsync();
    }
}