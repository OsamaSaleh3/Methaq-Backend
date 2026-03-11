using ErrorOr;
using MediatR;

namespace Methaq.Application.UseCases.Notifications.Commands.MarkAllAsRead;

public record MarkAllAsReadCommand(string UserId) : IRequest<ErrorOr<Success>>;