using ErrorOr;
using MediatR;
using Methaq.Domain.Employees.enums;

namespace Methaq.Application.Auth.Commands.RegisterEmployee;

public record RegisterEmployeeCommand(
    string FirstName,
    string SecondName,
    string ThirdName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword,
    string PhoneNumber,
    DateTime DateOfBirth,
    string? NationalId,
    string? Address,
    AcademicDegree Degree,
    string Specialization,
    string? IslamicQualifications,
    string? CurrentJob,
    EmployeeRole Role
) : IRequest<ErrorOr<string>>;
