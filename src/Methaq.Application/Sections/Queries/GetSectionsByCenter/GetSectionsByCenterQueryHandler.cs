using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Sections.Queries.GetSectionsByCenter;

public class GetSectionsByCenterQueryHandler : IRequestHandler<GetSectionsByCenterQuery, ErrorOr<List<SectionSummaryResponse>>>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IQuranCenterRepository _centerRepository;

    public GetSectionsByCenterQueryHandler(ISectionRepository sectionRepository, IQuranCenterRepository centerRepository)
    {
        _sectionRepository = sectionRepository;
        _centerRepository = centerRepository;
    }

    public async Task<ErrorOr<List<SectionSummaryResponse>>> Handle(GetSectionsByCenterQuery query, CancellationToken cancellationToken)
    {
        var center = await _centerRepository.GetByIdAsync(query.CenterId);
        if (center is null)
            return Error.NotFound("QuranCenter.NotFound", "Center not found.");

        var sections = await _sectionRepository.GetByCenterIdAsync(query.CenterId);

        var response = sections.Select(s => new SectionSummaryResponse(
            Id: s.Id,
            Name: s.Name,
            AcademicLevel: (int)s.AcademicLevel,
            Status: (int)s.Status,
            SupervisorName: s.Supervisor.User.FullName,
            StudentsCount: s.GetStudentCount()
        )).ToList();

        return response;
    }
}