using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.EnrollmentRequests.Queries.GetEnrollmentRequestsByCenter;

public class GetEnrollmentRequestsByCenterQueryHandler : IRequestHandler<GetEnrollmentRequestsByCenterQuery, ErrorOr<List<EnrollmentRequestResponse>>>
{
    private readonly IEnrollmentRequestRepository _enrollmentRepository;
    private readonly IQuranCenterRepository _centerRepository;

    public GetEnrollmentRequestsByCenterQueryHandler(IEnrollmentRequestRepository enrollmentRepository, IQuranCenterRepository centerRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _centerRepository = centerRepository;
    }

    public async Task<ErrorOr<List<EnrollmentRequestResponse>>> Handle(GetEnrollmentRequestsByCenterQuery query, CancellationToken cancellationToken)
    {
        var center = await _centerRepository.GetByIdAsync(query.CenterId);
        if (center is null)
            return Error.NotFound("QuranCenter.NotFound", "Center not found.");

        var requests = await _enrollmentRepository.GetByCenterIdAsync(query.CenterId);

        var response = requests.Select(r => new EnrollmentRequestResponse(
            RequestId: r.Id,
            StudentId: r.StudentId,
            StudentName: r.Student.User.FullName,
            StudentEmail: r.Student.User.Email!,
            Status: r.Status.ToString(),
            RejectionReason: r.RejectionReason,
            CreatedAt: r.CreatedAt
        )).ToList();

        return response;
    }
}