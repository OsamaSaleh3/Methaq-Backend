using ErrorOr;
using MediatR;

namespace Methaq.Application.Sections.Queries.GetSectionStudents;

public record GetSectionStudentsQuery(
    Guid SectionId
) : IRequest<ErrorOr<List<SectionStudentResponse>>>;

public record SectionStudentResponse(
    Guid StudentId,
    string FullName,
    string Email,
    string PhoneNumber,
    string AcademicLevel
);