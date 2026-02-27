using ErrorOr;
using MediatR;

namespace Methaq.Application.EnrollmentRequests.Commands.ApproveEnrollmentRequest;

public record ApproveEnrollmentRequestCommand(
    Guid RequestId
) : IRequest<ErrorOr<Success>>;