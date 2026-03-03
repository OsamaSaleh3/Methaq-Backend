using MediatR;
using Methaq.Application.AttendanceRecords.Commands.RecordAttendance;
using Methaq.Application.AttendanceRecords.Commands.UpdateAttendance;
using Methaq.Application.AttendanceRecords.Queries.GetAttendanceByLecture;
using Methaq.Application.AttendanceRecords.Queries.GetAttendanceByStudent;
using Methaq.Contracts.AttendanceRecords;
using Methaq.Domain.AttendanceRecords.enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Api.Controllers;

[Authorize]
public class AttendanceRecordsController : BaseController
{
    private readonly IMediator _mediator;

    public AttendanceRecordsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPost("lecture/{lectureId}")]
    public async Task<IActionResult> RecordAttendance(Guid lectureId, [FromBody] RecordAttendanceRequest request)
    {
        var command = new RecordAttendanceCommand(
            lectureId,
            request.StudentId,
            (AttendanceStatus)(request.Status),
            request.ExcuseReason,
            request.Notes);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpPut("{attendanceRecordId}")]
    public async Task<IActionResult> UpdateAttendance(Guid attendanceRecordId, [FromBody] UpdateAttendanceRequest request)
    {
        var command = new UpdateAttendanceCommand(
            attendanceRecordId,
            (AttendanceStatus)(request.Status),
            request.ExcuseReason,
            request.Notes);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager,Supervisor")]
    [HttpGet("lecture/{lectureId}")]
    public async Task<IActionResult> GetAttendanceByLecture(Guid lectureId)
    {
        var query = new GetAttendanceByLectureQuery(lectureId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetAttendanceByStudent(Guid studentId)
    {
        var query = new GetAttendanceByStudentQuery(studentId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }
}