using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Methaq.Application.Devices.Commands.RegisterPushToken;
using Methaq.Contracts.Devices;

namespace Methaq.Api.Controllers;

[Authorize]
public class DevicesController : BaseController
{
    private readonly ISender _sender;

    public DevicesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register-token")]
    public async Task<IActionResult> RegisterPushToken(RegisterPushTokenRequest request)
    {
        var command = new RegisterPushTokenCommand(
            UserId,
            request.Token,
            request.Platform);

        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}