using ErrorOr;
using MediatR;

namespace Methaq.Application.UseCases.Notifications.Commands.DeleteNotification;

public record DeleteNotificationCommand(string UserId, Guid NotificationId) : IRequest<ErrorOr<Success>>;