using ErrorOr;
using MediatR;
using Methaq.Domain.Sections.enums;

namespace Methaq.Application.UseCases.Students.Queries.GetMySection;

public record GetMySectionQuery(string UserId) : IRequest<ErrorOr<StudentSectionResponse>>;

public record StudentSectionResponse(
    Guid SectionId,
    string Name,
    AcademicLevel AcademicLevel,
    SectionStatus Status,
    Guid SupervisorId);