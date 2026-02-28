using MediatR;
using Methaq.Application.FinalReports.Commands.AddStudentReport;
using Methaq.Application.FinalReports.Commands.CreateFinalReport;
using Methaq.Application.FinalReports.Commands.SendFinalReportEmail;
using Methaq.Application.FinalReports.Queries.GetFinalReportBySection;
using Methaq.Contracts.FinalReports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Api.Controllers;

[Authorize]
public class FinalReportsController : BaseController
{
    private readonly IMediator _mediator;

    public FinalReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPost]
    public async Task<IActionResult> CreateFinalReport([FromBody] CreateFinalReportRequest request)
    {
        var command = new CreateFinalReportCommand(
            request.SectionId,
            request.GeneralNotes);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPost("{finalReportId}/students")]
    public async Task<IActionResult> AddStudentReport(Guid finalReportId, [FromBody] AddStudentReportRequest request)
    {
        var command = new AddStudentReportCommand(
            finalReportId,
            request.StudentId,
            request.ParticipationScore,
            request.BehaviorScore,
            request.SupervisorNotes);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPost("{finalReportId}/send-email")]
    public async Task<IActionResult> SendFinalReportEmail(Guid finalReportId)
    {
        var command = new SendFinalReportEmailCommand(finalReportId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("section/{sectionId}")]
    public async Task<IActionResult> GetFinalReportBySection(Guid sectionId)
    {
        var query = new GetFinalReportBySectionQuery(sectionId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }
}