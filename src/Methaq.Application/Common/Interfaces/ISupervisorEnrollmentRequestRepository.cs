using Methaq.Domain.SupervisorEnrollmentRequests;

namespace Methaq.Domain.SupervisorEnrollmentRequests;

public interface ISupervisorEnrollmentRequestRepository
{
    Task AddAsync(SupervisorEnrollmentRequest request, CancellationToken cancellationToken);
    Task<SupervisorEnrollmentRequest?> GetByIdWithDetailsAsync(Guid requestId, CancellationToken cancellationToken);
    Task<SupervisorEnrollmentRequest?> GetPendingByEmployeeAndCenterAsync(Guid employeeId, Guid centerId, CancellationToken cancellationToken);
    Task<List<SupervisorEnrollmentRequest>> GetPendingByCenterAsync(Guid centerId, CancellationToken cancellationToken);
}