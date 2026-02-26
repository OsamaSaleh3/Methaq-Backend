using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.QuranCenters.Queries.GetCenterById;

public class GetCenterByIdQueryHandler : IRequestHandler<GetCenterByIdQuery, ErrorOr<CenterResponse>>
{
    private readonly IQuranCenterRepository _centerRepository;

    public GetCenterByIdQueryHandler(IQuranCenterRepository centerRepository)
    {
        _centerRepository = centerRepository;
    }

    public async Task<ErrorOr<CenterResponse>> Handle(GetCenterByIdQuery query, CancellationToken cancellationToken)
    {
        var center = await _centerRepository.GetByIdWithDetailsAsync(query.CenterId);
        if (center is null)
            return Error.NotFound("QuranCenter.NotFound", "Center not found.");

        return new CenterResponse(
            Id: center.Id,
            Name: center.Name,
            Description: center.Description,
            Location: center.Location,
            PhoneNumber: center.PhoneNumber,
            Status: center.Status.ToString(),
            ManagerId: center.ManagerId,
            ManagerName: center.Manager.User.FullName,
            SectionsCount: center.Sections.Count,
            SupervisorsCount: center.Supervisors.Count,
            CreatedAt: center.CreatedAt);
    }
}