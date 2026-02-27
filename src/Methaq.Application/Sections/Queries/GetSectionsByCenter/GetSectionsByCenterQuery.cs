using ErrorOr;
using MediatR;

namespace Methaq.Application.Sections.Queries.GetSectionsByCenter;

public record GetSectionsByCenterQuery(
    Guid CenterId
) : IRequest<ErrorOr<List<SectionSummaryResponse>>>;

public record SectionSummaryResponse(
    Guid Id,
    string Name,
    string AcademicLevel,
    string Status,
    string SupervisorName,
    int StudentsCount
);