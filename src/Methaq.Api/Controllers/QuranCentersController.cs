using Methaq.Application.QuranCenters.Commands.AddSupervisor;
using Methaq.Application.QuranCenters.Commands.CloseCenter;
using Methaq.Application.QuranCenters.Commands.CreateCenter;
using Methaq.Application.QuranCenters.Commands.RemoveSupervisor;
using Methaq.Application.QuranCenters.Commands.TransferManagement;
using Methaq.Application.QuranCenters.Commands.UpdateCenterInfo;
using Methaq.Application.QuranCenters.Queries.GetAllCenters;
using Methaq.Application.QuranCenters.Queries.GetCenterById;
using Methaq.Application.QuranCenters.Queries.GetCenterSections;
using Methaq.Application.QuranCenters.Queries.GetCenterSupervisors;
using Methaq.Contracts.QuranCenters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Api.Controllers;

[Authorize]
public class QuranCentersController : BaseController
{
    private readonly IMediator _mediator;

    public QuranCentersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost]
    public async Task<IActionResult> CreateCenter([FromBody] CreateCenterRequest request)
    {
        var command = new CreateCenterCommand(
            request.Name,
            request.Description,
            request.Location,
            request.PhoneNumber,
            request.ManagerId);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager")]
    [HttpPut("{centerId}")]
    public async Task<IActionResult> UpdateCenterInfo(Guid centerId, [FromBody] UpdateCenterInfoRequest request)
    {
        var command = new UpdateCenterInfoCommand(
            centerId,
            request.Name,
            request.Description,
            request.Location,
            request.PhoneNumber);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager")]
    [HttpPost("{centerId}/supervisors")]
    public async Task<IActionResult> AddSupervisor(Guid centerId, [FromBody] AddSupervisorRequest request)
    {
        var command = new AddSupervisorCommand(centerId, request.SupervisorId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin,CenterManager")]
    [HttpDelete("{centerId}/supervisors/{supervisorId}")]
    public async Task<IActionResult> RemoveSupervisor(Guid centerId, Guid supervisorId)
    {
        var command = new RemoveSupervisorCommand(centerId, supervisorId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost("{centerId}/transfer-management")]
    public async Task<IActionResult> TransferManagement(Guid centerId, [FromBody] TransferManagementRequest request)
    {
        var command = new TransferManagementCommand(centerId, request.NewManagerId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost("{centerId}/close")]
    public async Task<IActionResult> CloseCenter(Guid centerId)
    {
        var command = new CloseCenterCommand(centerId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("{centerId}")]
    public async Task<IActionResult> GetCenterById(Guid centerId)
    {
        var query = new GetCenterByIdQuery(centerId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCenters()
    {
        var query = new GetAllCentersQuery();
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("{centerId}/supervisors")]
    public async Task<IActionResult> GetCenterSupervisors(Guid centerId)
    {
        var query = new GetCenterSupervisorsQuery(centerId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("{centerId}/sections")]
    public async Task<IActionResult> GetCenterSections(Guid centerId)
    {
        var query = new GetCenterSectionsQuery(centerId);
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }
}