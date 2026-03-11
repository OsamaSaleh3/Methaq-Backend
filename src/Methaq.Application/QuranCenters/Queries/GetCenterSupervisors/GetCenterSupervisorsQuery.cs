using ErrorOr;
using MediatR;

namespace Methaq.Application.QuranCenters.Queries.GetCenterSupervisors;

public record GetCenterSupervisorsQuery(
    Guid CenterId
) : IRequest<ErrorOr<List<SupervisorResponse>>>;

public record SupervisorResponse(
    Guid EmployeeId,
    string FullName,
    string Email,
    string Specialization,
    int EmploymentStatus
);