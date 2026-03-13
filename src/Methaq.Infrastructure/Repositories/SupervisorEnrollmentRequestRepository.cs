using Microsoft.EntityFrameworkCore;
using Methaq.Domain.SupervisorEnrollmentRequests;
using Methaq.Domain.SupervisorEnrollmentRequests.enums;
using Methaq.Infrastructure.Common.Persistence;

namespace Methaq.Infrastructure.Persistence.Repositories;

public class SupervisorEnrollmentRequestRepository : ISupervisorEnrollmentRequestRepository
{
    private readonly ApplicationDbContext _context;

    public SupervisorEnrollmentRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SupervisorEnrollmentRequest request, CancellationToken cancellationToken)
    { 
        await _context.SupervisorEnrollmentRequests.AddAsync(request, cancellationToken); 
    }

    public async Task<SupervisorEnrollmentRequest?> GetByIdWithDetailsAsync(Guid requestId, CancellationToken cancellationToken)
    {
        return await _context.SupervisorEnrollmentRequests
        .Include(r => r.Employee)
            .ThenInclude(e => e.User)
        .Include(r => r.Center)
        .FirstOrDefaultAsync(r => r.Id == requestId, cancellationToken);
    }

    public async Task<SupervisorEnrollmentRequest?> GetPendingByEmployeeAndCenterAsync(Guid employeeId, Guid centerId, CancellationToken cancellationToken)
    {
        return await _context.SupervisorEnrollmentRequests
            .FirstOrDefaultAsync(r =>
                r.EmployeeId == employeeId &&
                r.CenterId == centerId &&
                r.Status == SupervisorRequestStatus.Pending, cancellationToken);
    }
    public async Task<List<SupervisorEnrollmentRequest>> GetPendingByCenterAsync(Guid centerId, CancellationToken cancellationToken)
    {
        return await _context.SupervisorEnrollmentRequests
            .Include(r => r.Employee)
                .ThenInclude(e => e.User)
            .Include(r => r.Center)
            .Where(r => r.CenterId == centerId && r.Status == SupervisorRequestStatus.Pending)
            .ToListAsync(cancellationToken);
    }
}