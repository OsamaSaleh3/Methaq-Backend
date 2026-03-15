using Methaq.Application.Common.Interfaces;
using Methaq.Domain.FinalReports;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure.Repositories
{
    public class FinalReportRepository : IFinalReportRepository
    {
        private readonly ApplicationDbContext _context;

        public FinalReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FinalReport report)
        {
            await _context.FinalReports.AddAsync(report);
        }

        public async Task<FinalReport?> GetByIdAsync(Guid id)
        {
            return await _context.FinalReports.FindAsync(id);
        }

        public async Task<FinalReport?> GetByIdWithStudentsAsync(Guid id)
        {
            return await _context.FinalReports
                .Include(f=>f.StudentReports)
                    .ThenInclude(sr=>sr.Student)
                        .ThenInclude(s=>s.User)
                .Include(f=>f.Section)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<FinalReport?> GetBySectionIdAsync(Guid sectionId)
        {
            return await _context.FinalReports.FirstOrDefaultAsync(f => f.SectionId == sectionId);
        }

        public async Task<FinalReport?> GetBySectionIdWithDetailsAsync(Guid sectionId)
        {
            return await _context.FinalReports
                .Include(f => f.Section)
                .Include(f => f.StudentReports)
                    .ThenInclude(sr => sr.Student)
                        .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(f => f.SectionId == sectionId);
        }
    }
}
