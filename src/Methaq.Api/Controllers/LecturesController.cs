using MediatR;
using Methaq.Application.Lectures.Commands.CancelLecture;
using Methaq.Application.Lectures.Commands.CompleteLecture;
using Methaq.Application.Lectures.Commands.CreateLecture;
using Methaq.Application.Lectures.Commands.StartLecture;
using Methaq.Application.Lectures.Queries.GetLectureById;
using Methaq.Application.Lectures.Queries.GetLecturesByDate;
using Methaq.Application.Lectures.Queries.GetLecturesBySection;
using Methaq.Contracts.Lectures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Api.Controllers;

[Authorize]
public class LecturesController : BaseController
{
    private readonly IMediator _mediator;

    public LecturesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPost]
    public async Task<IActionResult> CreateLecture([FromBody] CreateLectureRequest request)
    {
        var command = new CreateLectureCommand(
            request.SectionId,
            request.Date,
            TimeOnly.Parse(request.StartTime),
            TimeOnly.Parse(request.EndTime));

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPost("{lectureId}/start")]
    public async Task<IActionResult> StartLecture(Guid lectureId)
    {
        var command = new StartLectureCommand(lectureId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPost("{lectureId}/complete")]
    public async Task<IActionResult> CompleteLecture(Guid lectureId, [FromBody] CompleteLectureRequest request)
    {
        var command = new CompleteLectureCommand(lectureId, request.Notes);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPost("{lectureId}/cancel")]
    public async Task<IActionResult> CancelLecture(Guid lectureId)
    {
        var command = new CancelLectureCommand(lectureId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("{lectureId}")]
    public async Task<IActionResult> GetLectureById(Guid lectureId)
    {
        var query = new GetLectureByIdQuery(lectureId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("section/{sectionId}")]
    public async Task<IActionResult> GetLecturesBySection(Guid sectionId)
    {
        var query = new GetLecturesBySectionQuery(sectionId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("{sectionId:guid}/lectures/by-date")]
    public async Task<IActionResult> GetLecturesByDate(Guid sectionId, [FromQuery] DateOnly date)
    {
        var query = new GetLecturesByDateQuery(sectionId, date);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }
}