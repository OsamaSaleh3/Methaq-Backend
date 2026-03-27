using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Contracts.Auth
{
    public record RegisterEmployeeRequest(
    string FirstName,
    string SecondName,
    string ThirdName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword,
    string PhoneNumber,
    DateOnly DateOfBirth,
    string? NationalId,
    string? Address,
    int Degree,
    string Specialization,
    string? IslamicQualifications,
    string? CurrentJob,
    int Role
);
}
