using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.UseCases.QuranCenters.Queries.GetMyCenterInfo;

public class GetMyCenterInfoQueryHandler : IRequestHandler<GetMyCenterInfoQuery, ErrorOr<CenterInfoResponse>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IQuranCenterRepository _centerRepository;

    public GetMyCenterInfoQueryHandler(IEmployeeRepository employeeRepository, IQuranCenterRepository centerRepository)
    {
        _employeeRepository = employeeRepository;
        _centerRepository = centerRepository;
    }

    public async Task<ErrorOr<CenterInfoResponse>> Handle(GetMyCenterInfoQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByUserIdAsync(request.UserId);
        if (employee is null)
            return Error.NotFound( "Employee.NotFound","Empkoyee not found.");

        if (employee.CenterId is null)
            return Error.NotFound("QuranCenter.NotAssigned","Employee is not assigned to any center.");

        var center = await _centerRepository.GetByIdWithDetailsAsync(employee.CenterId.Value);
        if (center is null)
            return Error.NotFound("QuranCenter.NotFound", "Center not found.");

        return new CenterInfoResponse(
            center.Id,
            center.Name,
            center.Description,
            center.Location,
            center.PhoneNumber,
            center.Status,
            center.ManagerId,
            center.Sections.Count,
            center.Supervisors.Count);
    }
}