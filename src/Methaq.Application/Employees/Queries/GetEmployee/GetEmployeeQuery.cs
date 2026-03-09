using ErrorOr;
using MediatR;

namespace Methaq.Application.Employees.Queries.GetEmployee;

public record GetEmployeeQuery(Guid EmployeeId) : IRequest<ErrorOr<EmployeeResponse>>;

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