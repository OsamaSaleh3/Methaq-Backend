using Methaq.Application.Common.Interfaces;
using Methaq.Domain.CenterEnrollmentRequests;
using Methaq.Domain.CenterEnrollmentRequests.enums;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Methaq.Infrastructure.Repositories;

public class EnrollmentRequestRepository : IEnrollmentRequestRepository
{
    private readonly ApplicationDbContext _context;

    public EnrollmentRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CenterEnrollmentRequest request, CancellationToken cancellationToken)
    {
        await _context.CenterEnrollmentRequests.AddAsync(request, cancellationToken);
    }

    public async Task<CenterEnrollmentRequest?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.CenterEnrollmentRequests
            .Include(r => r.Student)
                .ThenInclude(s => s.User)
            .Include(r => r.Center)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<CenterEnrollmentRequest?> GetPendingRequestAsync(Guid studentId, Guid centerId)
    {
        return await _context.CenterEnrollmentRequests
            .FirstOrDefaultAsync(r =>
                r.StudentId == studentId &&
                r.CenterId == centerId &&
                r.Status == EnrollmentRequestStatus.Pending);
    }

    public async Task<List<CenterEnrollmentRequest>> GetByCenterIdAsync(Guid centerId)
    {
        return await _context.CenterEnrollmentRequests
            .Include(r => r.Student)
                .ThenInclude(s => s.User)
            .Where(r => r.CenterId == centerId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<CenterEnrollmentRequest>> GetByStudentIdAsync(Guid studentId)
    {
        return await _context.CenterEnrollmentRequests
            .Include(r => r.Center)
            .Where(r => r.StudentId == studentId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }
}