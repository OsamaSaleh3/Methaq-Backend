using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.QuranCenters.Queries.GetCenterSections;

public class GetCenterSectionsQueryHandler : IRequestHandler<GetCenterSectionsQuery, ErrorOr<List<SectionSummaryResponse>>>
{
    private readonly IQuranCenterRepository _centerRepository;

    public GetCenterSectionsQueryHandler(IQuranCenterRepository centerRepository)
    {
        _centerRepository = centerRepository;
    }

    public async Task<ErrorOr<List<SectionSummaryResponse>>> Handle(GetCenterSectionsQuery query, CancellationToken cancellationToken)
    {
        var center = await _centerRepository.GetByIdWithDetailsAsync(query.CenterId);
        if (center is null)
            return Error.NotFound("QuranCenter.NotFound", "Center not found.");

        var response = center.Sections.Select(s => new SectionSummaryResponse(
            Id: s.Id,
            Name: s.Name,
            AcademicLevel: s.AcademicLevel.ToString(),
            Status: s.Status.ToString(),
            SupervisorName: s.Supervisor.User.FullName,
            StudentsCount: s.GetStudentCount()
        )).ToList();

        return response;
    }
}