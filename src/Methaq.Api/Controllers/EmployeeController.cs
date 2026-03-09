using MediatR;
using Methaq.Application.Employees.Commands.Reactivate;
using Methaq.Application.Employees.Commands.Resign;
using Methaq.Application.Employees.Commands.UpdateQualifications;
using Methaq.Contracts.Employees;
using Methaq.Domain.Employees.enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Api.Controllers;

[Authorize]
public class EmployeeController : BaseController
{
    private readonly ISender _mediator;

    public EmployeeController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{supervisorId}/qualifications")]
    public async Task<IActionResult> UpdateQualification(Guid supervisorId, [FromBody] UpdateQualificationsRequest request)
    {
        var command = new UpdateQualificationsCommand(
            supervisorId,
            request.AcademicDegree.HasValue ? (AcademicDegree)request.AcademicDegree.Value : null,
            request.Spesialization,
            request.IslamicAualification);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{supervisorId}/resign")]
    public async Task<IActionResult> Resign(Guid supervisorId)
    {
        var command = new ResignCommand(supervisorId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{supervisorId}/reactivate")]
    public async Task<IActionResult> Reactivate(Guid supervisorId)
    {
        var command = new ReactivateCommand(supervisorId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }
}