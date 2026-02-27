using Methaq.Domain.CenterEnrollmentRequests;

namespace Methaq.Application.Common.Interfaces;

public interface IEnrollmentRequestRepository
{
    Task<CenterEnrollmentRequest?> GetByIdWithDetailsAsync(Guid id);
    Task<CenterEnrollmentRequest?> GetPendingRequestAsync(Guid studentId, Guid centerId);
    Task<List<CenterEnrollmentRequest>> GetByCenterIdAsync(Guid centerId);
    Task<List<CenterEnrollmentRequest>> GetByStudentIdAsync(Guid studentId);
    Task AddAsync(CenterEnrollmentRequest request, CancellationToken cancellationToken);
    Task<CenterEnrollmentRequest?> GetApprovedRequestAsync(Guid studentId, Guid centerId);
}