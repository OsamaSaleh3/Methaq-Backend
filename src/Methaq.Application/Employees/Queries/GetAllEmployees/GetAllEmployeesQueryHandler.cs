using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ErrorOr<List<EmployeeResponse>>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<List<EmployeeResponse>>> Handle(GetAllEmployeesQuery query, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetAllWithUserAsync();

        return employees.Select(employee => new EmployeeResponse(
            employee.Id,
            employee.UserId,
            employee.User.FullName,
            employee.User.Email!,
            employee.User.PhoneNumber!,
            (int)employee.Degree,
            employee.Specialization,
            employee.IslamicQualifications,
            employee.CurrentJob,
            employee.HireDate,
            (int)employee.EmploymentStatus,
            (int)employee.Role,
            employee.CenterId)).ToList();
    }
}