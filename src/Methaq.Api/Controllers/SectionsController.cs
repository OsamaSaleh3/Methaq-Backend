using Methaq.Application.Sections.Commands.CreateSection;
using Methaq.Application.Sections.Commands.ChangeSupervisor;
using Methaq.Application.Sections.Commands.AddStudentToSection;
using Methaq.Application.Sections.Commands.RemoveStudentFromSection;
using Methaq.Application.Sections.Commands.CloseSection;
using Methaq.Application.Sections.Queries.GetSectionById;
using Methaq.Application.Sections.Queries.GetSectionsByCenter;
using Methaq.Application.Sections.Queries.GetSectionStudents;
using Methaq.Contracts.Sections;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Methaq.Domain.Sections.enums;

namespace Methaq.Api.Controllers;

public class SectionsController : BaseController
{
    private readonly IMediator _mediator;

    public SectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSection([FromBody] CreateSectionRequest request)
    {
        var command = new CreateSectionCommand(
            request.Name,
            Enum.Parse<AcademicLevel>(request.AcademicLevel),
            request.CenterId,
            request.SupervisorId,
            request.ScheduleDays.Select(d => Enum.Parse<DayOfWeek>(d)).ToList(),
            TimeOnly.Parse(request.StartTime),
            TimeOnly.Parse(request.EndTime));

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{sectionId}/supervisor")]
    public async Task<IActionResult> ChangeSupervisor(Guid sectionId, [FromBody] ChangeSupervisorRequest request)
    {
        var command = new ChangeSupervisorCommand(sectionId,request.NewSupervisorId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("{sectionId}/students")]
    public async Task<IActionResult> AddStudentToSection(Guid sectionId, [FromBody] AddStudentToSectionRequest request)
    {
        var command = new AddStudentToSectionCommand(sectionId,request.StudentId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{sectionId}/students/{studentId}")]
    public async Task<IActionResult> RemoveStudentFromSection(Guid sectionId, Guid studentId)
    {
        var command = new RemoveStudentFromSectionCommand(sectionId,studentId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("{sectionId}/close")]
    public async Task<IActionResult> CloseSection(Guid sectionId)
    {
        var command = new CloseSectionCommand(sectionId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("{sectionId}")]
    public async Task<IActionResult> GetSectionById(Guid sectionId)
    {
        var query = new GetSectionByIdQuery(sectionId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("center/{centerId}")]
    public async Task<IActionResult> GetSectionsByCenter(Guid centerId)
    {
        var query = new GetSectionsByCenterQuery(centerId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("{sectionId}/students")]
    public async Task<IActionResult> GetSectionStudents(Guid sectionId)
    {
        var query = new GetSectionStudentsQuery(sectionId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }
}
