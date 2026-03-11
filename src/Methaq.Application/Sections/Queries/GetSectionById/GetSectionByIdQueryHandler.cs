using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Sections.Queries.GetSectionById;

public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, ErrorOr<SectionDetailsResponse>>
{
    private readonly ISectionRepository _sectionRepository;

    public GetSectionByIdQueryHandler(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<ErrorOr<SectionDetailsResponse>> Handle(GetSectionByIdQuery query, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdWithDetailsAsync(query.SectionId);
        if (section is null)
            return Error.NotFound("Section.NotFound", "Section not found.");

        return new SectionDetailsResponse(
            Id: section.Id,
            Name: section.Name,
            AcademicLevel: (int)section.AcademicLevel,
            Status: (int)section.Status,
            CenterId: section.CenterId,
            CenterName: section.Center.Name,
            SupervisorId: section.SupervisorId,
            SupervisorName: section.Supervisor.User.FullName,
            ScheduleDays: section.Schedule.Days.Select(d => d.ToString()).ToList(),
            StartTime: section.Schedule.StartTime,
            EndTime: section.Schedule.EndTime,
            StudentsCount: section.GetStudentCount());
    }
}