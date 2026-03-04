using MediatR;
using Methaq.Application.EnrollmentRequests.Commands.ApproveEnrollmentRequest;
using Methaq.Application.EnrollmentRequests.Commands.RejectEnrollmentRequest;
using Methaq.Application.EnrollmentRequests.Commands.SubmitEnrollmentRequest;
using Methaq.Application.EnrollmentRequests.Queries.GetEnrollmentRequestsByCenter;
using Methaq.Application.EnrollmentRequests.Queries.GetMyEnrollmentRequests;
using Methaq.Contracts.EnrollmentRequests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Methaq.Api.Controllers;

public class EnrollmentRequestsController : BaseController
{
    private readonly IMediator _mediator;

    public EnrollmentRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitEnrollmentRequest([FromBody] SubmitEnrollmentRequestRequest request)
    {
        var studentIdClaim = User.FindFirstValue("StudentId");
        if (studentIdClaim is null)
            return Forbid();

        var studentId = Guid.Parse(studentIdClaim);

        var command = new SubmitEnrollmentRequestCommand(studentId, request.CenterId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("{requestId}/approve")]
    public async Task<IActionResult> ApproveEnrollmentRequest(Guid requestId)
    {
        var command = new ApproveEnrollmentRequestCommand(requestId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("{requestId}/reject")]
    public async Task<IActionResult> RejectEnrollmentRequest(Guid requestId, [FromBody] RejectEnrollmentRequestRequest request)
    {
        var command = new RejectEnrollmentRequestCommand(requestId,request.Reason);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("center/{centerId}")]
    public async Task<IActionResult> GetEnrollmentRequestsByCenter(Guid centerId)
    {
        var query = new GetEnrollmentRequestsByCenterQuery(centerId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("my-requests")]
    public async Task<IActionResult> GetMyEnrollmentRequests()
    {
        var studentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var query = new GetMyEnrollmentRequestsQuery(studentId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }
}
