using MediatR;
using Methaq.Api.Controllers;
using Methaq.Application.UseCases.Notifications.Commands.DeleteNotification;
using Methaq.Application.UseCases.Notifications.Commands.MarkAllAsRead;
using Methaq.Application.UseCases.Notifications.Commands.MarkAsRead;
using Methaq.Application.UseCases.Notifications.Queries.GetMyNotifications;
using Methaq.Application.UseCases.Notifications.Queries.GetUnreadCount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Presentation.Controllers;

[Authorize]
public class NotificationsController : BaseController
{
    private readonly ISender _sender;

    public NotificationsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyNotifications()
    {
        var query = new GetMyNotificationsQuery(UserId);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var query = new GetUnreadCountQuery(UserId);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpPut("{notificationId:guid}/mark-as-read")]
    public async Task<IActionResult> MarkAsRead(Guid notificationId)
    {
        var command = new MarkAsReadCommand(UserId, notificationId);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpPut("mark-all-as-read")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var command = new MarkAllAsReadCommand(UserId);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{notificationId:guid}")]
    public async Task<IActionResult> DeleteNotification(Guid notificationId)
    {
        var command = new DeleteNotificationCommand(UserId, notificationId);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}