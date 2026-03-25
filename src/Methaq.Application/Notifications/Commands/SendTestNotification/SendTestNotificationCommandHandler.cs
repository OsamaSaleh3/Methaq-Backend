using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Notifications.Commands.SendTestNotification;

public class SendTestNotificationCommandHandler : IRequestHandler<SendTestNotificationCommand, ErrorOr<Success>>
{
    private readonly INotificationService _notificationService;

    public SendTestNotificationCommandHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<ErrorOr<Success>> Handle(SendTestNotificationCommand request, CancellationToken cancellationToken)
    {
        await _notificationService.SendAsync(
            request.UserId,
            request.Title,
            request.Content,
            request.Type,
            request.RelatedEntityId);

        return Result.Success;
    }
}
