using ErrorOr;
using MediatR;

namespace Methaq.Application.Auth.Commands.RegisterStudent;

public record RegisterStudentCommand(
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
):IRequest<ErrorOr<string>>;