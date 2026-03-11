using ErrorOr;
using MediatR;

namespace Methaq.Application.UseCases.Notifications.Commands.MarkAsRead;

public record MarkAsReadCommand(string UserId, Guid NotificationId) : IRequest<ErrorOr<Success>>;