using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.SectionTasks.Queries.GetTasksByLecture;

public class GetTasksByLectureQueryHandler
    : IRequestHandler<GetTasksByLectureQuery, ErrorOr<List<SectionTaskResponse>>>
{
    private readonly ISectionTaskRepository _sectionTaskRepository;

    public GetTasksByLectureQueryHandler(ISectionTaskRepository sectionTaskRepository)
    {
        _sectionTaskRepository = sectionTaskRepository;
    }

    public async Task<ErrorOr<List<SectionTaskResponse>>> Handle(
        GetTasksByLectureQuery query,
        CancellationToken cancellationToken)
    {
        var tasks = await _sectionTaskRepository.GetByLectureIdAsync(query.LectureId);

        return tasks.Select(t => new SectionTaskResponse(
            t.Id,
            t.Title,
            t.Description,
            t.SectionId,
            t.LectureId,
            t.AssignedById,
            t.AssignedBy.User.FullName,
            t.FullMark,
            t.Types,
            t.Status,
            t.StudentId,
            t.Student?.User.FullName,
            t.Range,
            t.CreatedAt
        )).ToList();
    }
}