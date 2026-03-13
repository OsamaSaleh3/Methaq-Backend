using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Methaq.Application.SupervisorEnrollmentRequests.Commands.RequestToJoinCenter;
using Methaq.Application.SupervisorEnrollmentRequests.Commands.ApproveRequest;
using Methaq.Application.SupervisorEnrollmentRequests.Commands.RejectRequest;
using Methaq.Application.SupervisorEnrollmentRequests.Queries.GetPendingRequests;
using Methaq.Api.Controllers;
using Methaq.Contracts.SupervisorEnrollmentRequests;

namespace Methaq.Presentation.Controllers;

[Authorize]
public class SupervisorEnrollmentRequestsController : BaseController
{
    private readonly ISender _sender;

    public SupervisorEnrollmentRequestsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestToJoinCenter(RequestToJoinCenterRequest request)
    {
        var command = new RequestToJoinCenterCommand(UserId, request.CenterId);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{requestId:guid}/approve")]
    public async Task<IActionResult> ApproveRequest(Guid requestId)
    {
        var command = new ApproveRequestCommand(UserId, requestId);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{requestId:guid}/reject")]
    public async Task<IActionResult> RejectRequest(Guid requestId, RejectRequestRequest request)
    {
        var command = new RejectRequestCommand(UserId, requestId, request.Reason);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingRequests()
    {
        var query = new GetPendingRequestsQuery(UserId);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }
}