using ErrorOr;
using MediatR;

namespace Methaq.Application.SectionTasks.Queries.GetTasksHeatmap;

public record GetTasksHeatmapQuery(Guid SectionId) : IRequest<ErrorOr<List<TaskHeatmapResponse>>>;

public record TaskHeatmapResponse(
    DateOnly Date,
    int TasksCount);