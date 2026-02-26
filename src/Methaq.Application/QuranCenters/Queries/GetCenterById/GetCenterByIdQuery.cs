using ErrorOr;
using MediatR;

namespace Methaq.Application.QuranCenters.Queries.GetCenterById;

public record GetCenterByIdQuery(
    Guid CenterId
) : IRequest<ErrorOr<CenterResponse>>;

public record CenterResponse(
   Guid Id,
   string Name,
   string Description,
   string Location,
   string? PhoneNumber,
   string Status,
   Guid ManagerId,
   string ManagerName,
   int SectionsCount,
   int SupervisorsCount,
   DateTime CreatedAt
);
