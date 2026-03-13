using ErrorOr;
using MediatR;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.ApproveRequest;

public record ApproveRequestCommand(
    string UserId,
    Guid RequestId) : IRequest<ErrorOr<Success>>;