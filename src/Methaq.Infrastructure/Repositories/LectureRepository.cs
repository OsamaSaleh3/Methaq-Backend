using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Lectures;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private readonly ApplicationDbContext _context;

        public LectureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Lecture lecture, CancellationToken cancellationToken)
        {
            await _context.Lectures.AddAsync(lecture, cancellationToken);
        }

        public async Task<Lecture?> GetByIdAsync(Guid id)
        {
            return await _context.Lectures.FindAsync(id);
        }

        public async Task<Lecture?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Lectures
                .Include(l => l.Section)
                .Include(l => l.SectionTasks)
                .Include(l => l.AttendanceRecords)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Lecture>> GetBySectionIdAsync(Guid sectionId)
        {
            return await _context.Lectures
                .Where(l => l.SectionId == sectionId)
                .ToListAsync();
        }
    }
}
