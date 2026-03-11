using ErrorOr;
using MediatR;

namespace Methaq.Application.UseCases.Students.Queries.GetAllStudents;

public record GetAllStudentsQuery : IRequest<ErrorOr<List<StudentResponse>>>;

public record StudentResponse(
    Guid Id,
    string UserId,
    string FullName,
    string Email,
    string? PhoneNumber,
    string GuardianName,
    string GuardianPhone,
    string? GuardianEmail,
    string AcademicLevel,
    Guid? CenterId,
    Guid? SectionId);