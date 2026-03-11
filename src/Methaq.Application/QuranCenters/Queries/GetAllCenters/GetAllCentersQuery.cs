using ErrorOr;
using MediatR;

namespace Methaq.Application.QuranCenters.Queries.GetAllCenters;

public record GetAllCentersQuery() : IRequest<ErrorOr<List<CenterSummaryResponse>>>;

public record CenterSummaryResponse(
    Guid Id,
    string Name,
    string Location,
    int Status,
    string ManagerName,
    int SectionsCount
);