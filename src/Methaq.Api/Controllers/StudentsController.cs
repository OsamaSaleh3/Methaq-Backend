using MediatR;
using Methaq.Api.Controllers;
using Methaq.Application.UseCases.Students.Commands.RemoveStudentFromCenter;
using Methaq.Application.UseCases.Students.Commands.UpdateGuardianInfo;
using Methaq.Application.UseCases.Students.Queries.GetAllStudents;
using Methaq.Application.UseCases.Students.Queries.GetMyFinalReport;
using Methaq.Application.UseCases.Students.Queries.GetMyLectures;
using Methaq.Application.UseCases.Students.Queries.GetMySection;
using Methaq.Application.UseCases.Students.Queries.GetMyTasks;
using Methaq.Application.UseCases.Students.Queries.GetStudentById;
using Methaq.Application.UseCases.Students.Queries.GetStudentsByCenter;
using Methaq.Contracts.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Presentation.Controllers;

[Authorize]
public class StudentsController : BaseController
{
    private readonly ISender _sender;

    public StudentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{studentId:guid}")]
    public async Task<IActionResult> GetStudentById(Guid studentId)
    {
        var query = new GetStudentByIdQuery(studentId);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var query = new GetAllStudentsQuery();
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpGet("center/{centerId:guid}")]
    public async Task<IActionResult> GetStudentsByCenter(Guid centerId)
    {
        var query = new GetStudentsByCenterQuery(centerId);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpGet("me/section")]
    public async Task<IActionResult> GetMySection()
    {
        var query = new GetMySectionQuery(UserId);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpGet("me/final-report")]
    public async Task<IActionResult> GetMyFinalReport()
    {
        var query = new GetMyFinalReportQuery(UserId);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpGet("me/lectures")]
    public async Task<IActionResult> GetMyLectures()
    {
        var query = new GetMyLecturesQuery(UserId);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpGet("me/tasks")]
    public async Task<IActionResult> GetMyTasks()
    {
        var query = new GetMyTasksQuery(UserId);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpPut("me/guardian-info")]
    public async Task<IActionResult> UpdateGuardianInfo(UpdateGuardianInfoRequest request)
    {
        var command = new UpdateGuardianInfoCommand(
            UserId,
            request.GuardianName,
            request.GuardianPhone,
            request.GuardianEmail);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{studentId:guid}/center")]
    public async Task<IActionResult> RemoveStudentFromCenter(Guid studentId)
    {
        var command = new RemoveStudentFromCenterCommand(studentId);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}