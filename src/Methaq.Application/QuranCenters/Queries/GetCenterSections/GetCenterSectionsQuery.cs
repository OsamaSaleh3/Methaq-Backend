using ErrorOr;
using MediatR;

namespace Methaq.Application.QuranCenters.Queries.GetCenterSections;

public record GetCenterSectionsQuery(
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