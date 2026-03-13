using ErrorOr;
using MediatR;

namespace Methaq.Application.SupervisorEnrollmentRequests.Queries.GetPendingRequests;

public record GetPendingRequestsQuery(string UserId) : IRequest<ErrorOr<List<SupervisorRequestResponse>>>;

public record SupervisorRequestResponse(
    Guid Id,
    Guid EmployeeId,
    string EmployeeName,
    string EmployeeEmail,
    Guid CenterId,
    string CenterName,
    int Status,
    DateTime CreatedAt);