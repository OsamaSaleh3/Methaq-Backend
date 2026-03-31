using MediatR;
using Methaq.Application.SectionTasks.Commands.CreateSectionTask;
using Methaq.Application.SectionTasks.Commands.EvaluateStudent;
using Methaq.Application.SectionTasks.Queries.GetStudentEvaluations;
using Methaq.Application.SectionTasks.Queries.GetTasksByDate;
using Methaq.Application.SectionTasks.Queries.GetTasksByLecture;
using Methaq.Application.SectionTasks.Queries.GetTasksHeatmap;
using Methaq.Contracts.SectionTasks;
using Methaq.Domain.SectionTasks.enums;
using Methaq.Domain.SectionTasks.ValueObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Api.Controllers;

[Authorize]
public class SectionTasksController : BaseController
{
    private readonly IMediator _mediator;

    public SectionTasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPost]
    public async Task<IActionResult> CreateSectionTask([FromBody] CreateSectionTaskRequest request)
    {
        var command = new CreateSectionTaskCommand(
        request.Title,
        request.Description,
        request.SectionId,
        request.LectureId,
        request.AssignedById,
        request.FullMark,
        (TaskTypes)(request.Types),
        request.StudentId,
        request.Range is null ? null : new QuranRange(
        request.Range.Volume,
        request.Range.SurahName,
        request.Range.StartPage,
        request.Range.EndPage,
        request.Range.StartAyah,
        request.Range.EndAyah)
);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPost("{sectionTaskId}/evaluate")]
    public async Task<IActionResult> EvaluateStudent(Guid sectionTaskId, [FromBody] EvaluateStudentRequest request)
    {
        var command = new EvaluateStudentCommand(
            sectionTaskId,
            request.StudentId,
            request.AchievedMark,
            request.Notes);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpGet("lecture/{lectureId}")]
    public async Task<IActionResult> GetTasksByLecture(Guid lectureId)
    {
        var query = new GetTasksByLectureQuery(lectureId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("student/{studentId}/evaluations")]
    public async Task<IActionResult> GetStudentEvaluations(Guid studentId)
    {
        var query = new GetStudentEvaluationsQuery(studentId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }


    [HttpGet("{sectionId:guid}/tasks/by-date")]
    public async Task<IActionResult> GetTasksByDate(Guid sectionId, [FromQuery] DateOnly date)
    {
        var query = new GetTasksByDateQuery(sectionId, date);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("{sectionId:guid}/tasks/heatmap")]
    public async Task<IActionResult> GetTasksHeatmap(Guid sectionId)
    {
        var query = new GetTasksHeatmapQuery(sectionId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }
}