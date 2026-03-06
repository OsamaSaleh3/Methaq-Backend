using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Sections;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly ApplicationDbContext _context;

        public SectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Section section, CancellationToken cancellationToken)
        {
            await _context.Sections.AddAsync(section, cancellationToken);
        }

        public async Task<List<Section>> GetByCenterIdAsync(Guid centerId)
        {
            return await _context.Sections
                .Include(c=>c.Supervisor)
                    .ThenInclude(s=>s.User)
                .Where(s => s.CenterId == centerId).ToListAsync();
        }

        public async Task<Section?> GetByIdAsync(Guid id)
        {
           return await _context.Sections.FindAsync(id);
        }

        public async Task<Section?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Sections
                .Include(s => s.Supervisor)
                    .ThenInclude(s => s.User)
                .Include(s => s.Center)
                .Include(s => s.Students)
                .Include(s => s.Lectures)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Section?> GetByIdWithStudentsAsync(Guid id)
        {
            return await _context.Sections
                .Include(s => s.Students)
                    .ThenInclude (s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
