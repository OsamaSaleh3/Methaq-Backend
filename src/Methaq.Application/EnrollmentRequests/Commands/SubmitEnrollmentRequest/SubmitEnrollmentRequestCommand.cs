using ErrorOr;
using MediatR;

namespace Methaq.Application.EnrollmentRequests.Commands.SubmitEnrollmentRequest;

public record SubmitEnrollmentRequestCommand(
    Guid StudentId,
    Guid CenterId
) : IRequest<ErrorOr<Guid>>;