using ErrorOr;
using MediatR;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.RejectRequest;

public record RejectRequestCommand(
    string UserId,
    Guid RequestId,
    string? Reason) : IRequest<ErrorOr<Success>>;