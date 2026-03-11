using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.EnrollmentRequests.Queries.GetMyEnrollmentRequests;

public class GetMyEnrollmentRequestsQueryHandler : IRequestHandler<GetMyEnrollmentRequestsQuery, ErrorOr<List<MyEnrollmentRequestResponse>>>
{
    private readonly IEnrollmentRequestRepository _enrollmentRepository;

    public GetMyEnrollmentRequestsQueryHandler(IEnrollmentRequestRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<ErrorOr<List<MyEnrollmentRequestResponse>>> Handle(GetMyEnrollmentRequestsQuery query, CancellationToken cancellationToken)
    {
        var requests = await _enrollmentRepository.GetByStudentIdAsync(query.StudentId);

        var response = requests.Select(r => new MyEnrollmentRequestResponse(
            RequestId: r.Id,
            CenterId: r.CenterId,
            CenterName: r.Center.Name,
            CenterLocation: r.Center.Location,
            Status: (int)r.Status,
            RejectionReason: r.RejectionReason,
            CreatedAt: r.CreatedAt
        )).ToList();

        return response;
    }
}