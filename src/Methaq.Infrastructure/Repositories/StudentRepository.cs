using Methaq.Application.Common.Interfaces;
using Methaq.Domain.FinalReports;
using Methaq.Domain.Lectures;
using Methaq.Domain.SectionTasks;
using Methaq.Domain.Students;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Methaq.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Student>> GetAllWithUserAsync(CancellationToken cancellationToken)
    {
        return _context.Students
            .Include(s => s.User)
            .ToListAsync(cancellationToken);
    }

    public Task<List<Student>> GetByCenterAsync(Guid centerId, CancellationToken cancellationToken)
    {
        return _context.Students
            .Include(s => s.User)
            .Where(s=>s.CenterId==centerId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Student?> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id,cancellationToken);
    }

    public async Task<Student?> GetByIdWithUserAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id,cancellationToken);
    }

    public async Task<Student?> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.Students
        .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
    }

    public async Task<Student?> GetByUserIdWithSectionAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.Students
        .Include(s => s.Section)
        .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
    }

    public async Task<StudentFinalReport?> GetMyFinalReportAsync(string userId, CancellationToken cancellationToken)
    {
         return await  _context.StudentFinalReports
        .Include(r => r.Student)
        .Where(r => r.Student.UserId == userId)
        .OrderByDescending(r => r.FinalReport.GeneratedAt)
        .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Lecture>> GetMyLecturesAsync(Guid studentId, CancellationToken cancellationToken)
    {
        return await _context.Lectures
        .Include(l => l.AttendanceRecords)
        .Where(l => l.Section.Students.Any(s => s.Id == studentId))
        .OrderByDescending(l => l.Date)
        .ToListAsync(cancellationToken);
    }

    public async Task<List<SectionTask>> GetMyTasksAsync(Guid studentId, CancellationToken cancellationToken)
    {
        return await _context.SectionTasks
        .Include(t => t.Evaluations)
        .Where(t => t.Section.Students.Any(s => s.Id == studentId))
        .ToListAsync(cancellationToken);
    }
}