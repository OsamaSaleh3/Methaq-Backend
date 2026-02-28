using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.SectionTasks.Queries.GetStudentEvaluations;

public class GetStudentEvaluationsQueryHandler
    : IRequestHandler<GetStudentEvaluationsQuery, ErrorOr<List<StudentEvaluationResponse>>>
{
    private readonly ISectionTaskRepository _sectionTaskRepository;

    public GetStudentEvaluationsQueryHandler(ISectionTaskRepository sectionTaskRepository)
    {
        _sectionTaskRepository = sectionTaskRepository;
    }

    public async Task<ErrorOr<List<StudentEvaluationResponse>>> Handle(
        GetStudentEvaluationsQuery query,
        CancellationToken cancellationToken)
    {
        var evaluations = await _sectionTaskRepository.GetEvaluationsByStudentIdAsync(query.StudentId);

        return evaluations.Select(e => new StudentEvaluationResponse(
            e.SectionTaskId,
            e.SectionTask.Title,
            e.SectionTask.FullMark,
            e.AchievedMark,
            e.Notes,
            e.EvaluatedAt
        )).ToList();
    }
}