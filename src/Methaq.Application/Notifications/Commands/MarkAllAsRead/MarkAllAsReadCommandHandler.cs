using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.UseCases.Notifications.Commands.MarkAllAsRead;

public class MarkAllAsReadCommandHandler : IRequestHandler<MarkAllAsReadCommand, ErrorOr<Success>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkAllAsReadCommandHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(MarkAllAsReadCommand request, CancellationToken cancellationToken)
    {
        var notifications = await _notificationRepository.GetUnreadByUserIdAsync(request.UserId);

        foreach (var notification in notifications)
            notification.MarkAsRead();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}