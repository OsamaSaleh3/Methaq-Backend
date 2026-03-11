using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.UseCases.Employees.Queries.GetEmployeesByCenter;

public class GetEmployeesByCenterQueryHandler : IRequestHandler<GetEmployeesByCenterQuery, ErrorOr<List<EmployeeResponse>>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesByCenterQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<List<EmployeeResponse>>> Handle(GetEmployeesByCenterQuery request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetByCenterAsync(request.CenterId, cancellationToken);

        return employees.Select(e => new EmployeeResponse(
            e.Id,
            e.UserId,
            e.User.FullName,
            e.User.Email!,
            e.User.PhoneNumber!,
            (int)e.Degree,
            e.Specialization,
            e.IslamicQualifications,
            e.CurrentJob,
            e.HireDate,
            (int)e.EmploymentStatus,
            (int)e.Role,
            e.CenterId)).ToList();
    }
}