using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Methaq.Application.GroupChats.Commands.SendMessage;
using Methaq.Application.GroupChats.Commands.EditMessage;
using Methaq.Application.GroupChats.Commands.DeleteMessage;
using Methaq.Application.GroupChats.Queries.GetChatBySection;
using Methaq.Application.GroupChats.Queries.GetMessages;
using Methaq.Contracts.GroupChats;

namespace Methaq.Api.Controllers;

[Authorize]
public class GroupChatsController : BaseController
{
    private readonly IMediator _mediator;

    public GroupChatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{groupChatId}/messages")]
    public async Task<IActionResult> SendMessage(Guid groupChatId, [FromBody] SendMessageRequest request)
    {
        var command = new SendMessageCommand(
            groupChatId,
            UserId,
            request.Content,
            request.AttachmentUrl);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPut("messages/{messageId}")]
    public async Task<IActionResult> EditMessage(Guid messageId, [FromBody] EditMessageRequest request)
    {
        var command = new EditMessageCommand(
            messageId,
            UserId,
            request.NewContent);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("messages/{messageId}")]
    public async Task<IActionResult> DeleteMessage(Guid messageId)
    {
        var command = new DeleteMessageCommand(messageId, UserId);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("section/{sectionId}")]
    public async Task<IActionResult> GetChatBySection(Guid sectionId)
    {
        var query = new GetChatBySectionQuery(sectionId);

        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("{groupChatId}/messages")]
    public async Task<IActionResult> GetMessages(Guid groupChatId)
    {
        var query = new GetMessagesQuery(groupChatId);

        var result = await _mediator.Send(query);
        return HandleResult(result);
    }
}