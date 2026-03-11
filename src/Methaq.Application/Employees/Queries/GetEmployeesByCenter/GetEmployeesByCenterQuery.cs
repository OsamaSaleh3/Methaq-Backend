using ErrorOr;
using MediatR;

namespace Methaq.Application.UseCases.Employees.Queries.GetEmployeesByCenter;

public record GetEmployeesByCenterQuery(Guid CenterId) : IRequest<ErrorOr<List<EmployeeResponse>>>;

public record EmployeeResponse(
    Guid Id,
    string UserId,
    string FullName,
    string Email,
    string PhoneNumber,
    string Degree,
    string Specialization,
    string? IslamicQualifications,
    string? CurrentJob,
    DateTime HireDate,
    string EmploymentStatus,
    string Role,
    Guid? CenterId
);