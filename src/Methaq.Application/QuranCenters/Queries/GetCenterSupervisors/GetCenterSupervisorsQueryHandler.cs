using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.QuranCenters.Queries.GetCenterSupervisors;

public class GetCenterSupervisorsQueryHandler : IRequestHandler<GetCenterSupervisorsQuery, ErrorOr<List<SupervisorResponse>>>
{
    private readonly IQuranCenterRepository _centerRepository;

    public GetCenterSupervisorsQueryHandler(IQuranCenterRepository centerRepository)
    {
        _centerRepository = centerRepository;
    }

    public async Task<ErrorOr<List<SupervisorResponse>>> Handle(GetCenterSupervisorsQuery query, CancellationToken cancellationToken)
    {
        var center = await _centerRepository.GetByIdWithDetailsAsync(query.CenterId);
        if (center is null)
            return Error.NotFound("QuranCenter.NotFound", "Center not found.");

        var response = center.Supervisors.Select(s => new SupervisorResponse(
            EmployeeId: s.Id,
            FullName: s.User.FullName,
            Email: s.User.Email!,
            Specialization: s.Specialization,
            EmploymentStatus: (int)s.EmploymentStatus
        )).ToList();

        return response;
    }
}