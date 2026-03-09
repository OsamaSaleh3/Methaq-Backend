using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Employees.Queries.GetEmployee;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, ErrorOr<EmployeeResponse>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<EmployeeResponse>> Handle(GetEmployeeQuery query, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdWithUserAsync(query.EmployeeId);
        if (employee is null)
            return Error.NotFound("Employee.NotFound", "Employee not found.");

        return new EmployeeResponse(
            employee.Id,
            employee.UserId,
            employee.User.FullName,
            employee.User.Email!,
            employee.User.PhoneNumber!,
            employee.Degree.ToString(),
            employee.Specialization,
            employee.IslamicQualifications,
            employee.CurrentJob,
            employee.HireDate,
            employee.EmploymentStatus.ToString(),
            employee.Role.ToString(),
            employee.CenterId);
    }
}