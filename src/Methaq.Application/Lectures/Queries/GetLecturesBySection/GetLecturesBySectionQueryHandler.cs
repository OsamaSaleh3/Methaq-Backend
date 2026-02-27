using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Lectures.Queries.GetLecturesBySection;

public class GetLecturesBySectionQueryHandler : IRequestHandler<GetLecturesBySectionQuery, ErrorOr<List<LectureSummaryResponse>>>
{
    private readonly ILectureRepository _lectureRepository;
    private readonly ISectionRepository _sectionRepository;

    public GetLecturesBySectionQueryHandler(
        ILectureRepository lectureRepository,
        ISectionRepository sectionRepository)
    {
        _lectureRepository = lectureRepository;
        _sectionRepository = sectionRepository;
    }

    public async Task<ErrorOr<List<LectureSummaryResponse>>> Handle(GetLecturesBySectionQuery query, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdAsync(query.SectionId);
        if (section is null)
            return Error.NotFound("Section.NotFound", "Section not found.");

        var lectures = await _lectureRepository.GetBySectionIdAsync(query.SectionId);

        var response = lectures.Select(l => new LectureSummaryResponse(
            Id: l.Id,
            Date: l.Date,
            StartTime: l.StartTime.ToString(),
            EndTime: l.EndTime.ToString(),
            Status: l.Status.ToString(),
            AttendanceCount: l.AttendanceRecords.Count,
            TasksCount: l.SectionTasks.Count
        )).ToList();

        return response;
    }
}