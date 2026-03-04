using Methaq.Application.Common.Interfaces;
using Methaq.Domain.AttendanceRecords;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Methaq.Infrastructure.Repositories
{
    public class AttendanceRecordRepository : IAttendanceRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AttendanceRecord record)
        {
            await _context.AttendanceRecords.AddAsync(record);
        }

        public async Task<bool> ExistsAsync(Guid lectureId, Guid studentId)
        {
            return await _context.AttendanceRecords.AnyAsync(ar => ar.LectureId == lectureId && ar.StudentId == studentId);
        }

        public async Task<AttendanceRecord?> GetByIdAsync(Guid id)
        {
            return await _context.AttendanceRecords.FindAsync(id);
        }

        public Task<List<AttendanceRecord>> GetByLectureIdAsync(Guid lectureId)
        {
            return _context.AttendanceRecords
                .Include(a=>a.Student)
                    .ThenInclude(s=>s.User)
                .Where(ar => ar.LectureId == lectureId)
                .ToListAsync();
        }

        public async Task<List<AttendanceRecord>> GetByStudentIdAsync(Guid studentId)
        {
            return await _context.AttendanceRecords
                .Where(ar => ar.StudentId == studentId)
                .ToListAsync();
        }
    }
}
