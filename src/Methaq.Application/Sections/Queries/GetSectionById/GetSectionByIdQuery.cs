using ErrorOr;
using MediatR;

namespace Methaq.Application.Sections.Queries.GetSectionById;

public record GetSectionByIdQuery(
    Guid SectionId
) : IRequest<ErrorOr<SectionDetailsResponse>>;

public record SectionDetailsResponse(
    Guid Id,
    string Name,
    string AcademicLevel,
    string Status,
    Guid CenterId,
    string CenterName,
    Guid SupervisorId,
    string SupervisorName,
    List<string> ScheduleDays,
    string StartTime,
    string EndTime,
    int StudentsCount
);