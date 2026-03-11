using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.QuranCenters.Queries.GetAllCenters;

public class GetAllCentersQueryHandler : IRequestHandler<GetAllCentersQuery, ErrorOr<List<CenterSummaryResponse>>>
{
    private readonly IQuranCenterRepository _centerRepository;

    public GetAllCentersQueryHandler(IQuranCenterRepository centerRepository)
    {
        _centerRepository = centerRepository;
    }

    public async Task<ErrorOr<List<CenterSummaryResponse>>> Handle(GetAllCentersQuery query, CancellationToken cancellationToken)
    {
        var centers = await _centerRepository.GetAllWithDetailsAsync();

        var response = centers.Select(center => new CenterSummaryResponse(
            Id: center.Id,
            Name: center.Name,
            Location: center.Location,
            Status: (int)center.Status,
            ManagerName: center.Manager.User.FullName,
            SectionsCount: center.Sections.Count
        )).ToList();

        return response;
    }
}