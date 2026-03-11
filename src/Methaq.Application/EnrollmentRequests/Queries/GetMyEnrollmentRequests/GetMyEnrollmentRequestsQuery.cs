using ErrorOr;
using MediatR;

namespace Methaq.Application.EnrollmentRequests.Queries.GetMyEnrollmentRequests;

public record GetMyEnrollmentRequestsQuery(
    Guid StudentId
) : IRequest<ErrorOr<List<MyEnrollmentRequestResponse>>>;

public record MyEnrollmentRequestResponse(
    Guid RequestId,
    Guid CenterId,
    string CenterName,
    string CenterLocation,
    int Status,
    string? RejectionReason,
    DateTime CreatedAt
);