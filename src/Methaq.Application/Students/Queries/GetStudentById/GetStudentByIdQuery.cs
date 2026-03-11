using ErrorOr;
using MediatR;

namespace Methaq.Application.UseCases.Students.Queries.GetStudentById;

public record GetStudentByIdQuery(Guid StudentId) : IRequest<ErrorOr<StudentResponse>>;

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