using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Contracts.Auth
{
    public record RegisterStudentRequest(
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
    string GuardianName,
    string GuardianPhone,
    string? GuardianEmail,
    string AcademicLevel
);
}
