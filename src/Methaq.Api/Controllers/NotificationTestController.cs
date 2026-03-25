using MediatR;
using Methaq.Application.Notifications.Commands.SendTestNotification;
using Methaq.Contracts.Notifications;
using Methaq.Domain.Notifications.enums;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Api.Controllers;

public class NotificationTestController : BaseController
{
    private readonly ISender _sender;

    public NotificationTestController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] SendTestNotificationRequest request)
    {
        var command = new SendTestNotificationCommand(
            request.UserId,
            request.Title,
            request.Content,
            (NotificationType)request.Type,
            request.RelatedEntityId);

        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}
