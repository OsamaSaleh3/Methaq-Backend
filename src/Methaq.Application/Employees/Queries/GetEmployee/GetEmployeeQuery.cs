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
    int Degree,
    string Specialization,
    string? IslamicQualifications,
    string? CurrentJob,
    DateTime HireDate,
    int EmploymentStatus,
    int Role,
    Guid? CenterId
);