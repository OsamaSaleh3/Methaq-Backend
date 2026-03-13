using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Employees;
using Methaq.Domain.SupervisorEnrollmentRequests;

namespace Methaq.Application.SupervisorEnrollmentRequests.Queries.GetPendingRequests;

public class GetPendingRequestsQueryHandler : IRequestHandler<GetPendingRequestsQuery, ErrorOr<List<SupervisorRequestResponse>>>
{
    private readonly ISupervisorEnrollmentRequestRepository _requestRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetPendingRequestsQueryHandler(
        ISupervisorEnrollmentRequestRepository requestRepository,
        IEmployeeRepository employeeRepository)
    {
        _requestRepository = requestRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<List<SupervisorRequestResponse>>> Handle(GetPendingRequestsQuery request, CancellationToken cancellationToken)
    {
        var manager = await _employeeRepository.GetByUserIdAsync(request.UserId);
        if (manager is null || !manager.IsManager() || manager.CenterId is null)
            return SupervisorEnrollmentRequestErrors.NotCenterManager;

        var requests = await _requestRepository.GetPendingByCenterAsync(manager.CenterId.Value, cancellationToken);

        return requests.Select(r => new SupervisorRequestResponse(
            r.Id,
            r.EmployeeId,
            r.Employee.User.FullName,
            r.Employee.User.Email!,
            r.CenterId,
            r.Center.Name,
            (int)r.Status,
            r.CreatedAt)).ToList();
    }
}