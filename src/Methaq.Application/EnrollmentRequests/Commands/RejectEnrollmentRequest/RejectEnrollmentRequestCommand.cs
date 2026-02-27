using ErrorOr;
using MediatR;

namespace Methaq.Application.EnrollmentRequests.Commands.RejectEnrollmentRequest;

public record RejectEnrollmentRequestCommand(
    Guid RequestId,
    string? Reason
) : IRequest<ErrorOr<Success>>;