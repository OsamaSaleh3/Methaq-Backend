using ErrorOr;
using MediatR;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.RequestToJoinCenter;

public record RequestToJoinCenterCommand(
    string UserId,
    Guid CenterId) : IRequest<ErrorOr<Guid>>;