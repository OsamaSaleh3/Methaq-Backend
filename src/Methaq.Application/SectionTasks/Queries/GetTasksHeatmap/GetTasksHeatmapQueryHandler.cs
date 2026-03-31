using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.SectionTasks.Queries.GetTasksHeatmap;

public class GetTasksHeatmapQueryHandler : IRequestHandler<GetTasksHeatmapQuery, ErrorOr<List<TaskHeatmapResponse>>>
{
    private readonly ISectionTaskRepository _sectionTaskRepository;

    public GetTasksHeatmapQueryHandler(ISectionTaskRepository sectionTaskRepository)
    {
        _sectionTaskRepository = sectionTaskRepository;
    }

    public async Task<ErrorOr<List<TaskHeatmapResponse>>> Handle(GetTasksHeatmapQuery query, CancellationToken cancellationToken)
    {
        var heatmap = await _sectionTaskRepository.GetTasksHeatmapAsync(query.SectionId);
        return heatmap;
    }
}