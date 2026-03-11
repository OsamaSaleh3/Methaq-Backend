using ErrorOr;
using MediatR;

namespace Methaq.Application.EnrollmentRequests.Queries.GetEnrollmentRequestsByCenter;

public record GetEnrollmentRequestsByCenterQuery(
    Guid CenterId
) : IRequest<ErrorOr<List<EnrollmentRequestResponse>>>;

public record EnrollmentRequestResponse(
    Guid RequestId,
    Guid StudentId,
    string StudentName,
    string StudentEmail,
    int Status,
    string? RejectionReason,
    DateTime CreatedAt
);