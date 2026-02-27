using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.CenterEnrollmentRequests;

namespace Methaq.Application.EnrollmentRequests.Commands.SubmitEnrollmentRequest;

public class SubmitEnrollmentRequestCommandHandler : IRequestHandler<SubmitEnrollmentRequestCommand, ErrorOr<Guid>>
{
    private readonly IEnrollmentRequestRepository _enrollmentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IQuranCenterRepository _centerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SubmitEnrollmentRequestCommandHandler(IEnrollmentRequestRepository enrollmentRepository, IStudentRepository studentRepository, IQuranCenterRepository centerRepository, IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
        _centerRepository = centerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(SubmitEnrollmentRequestCommand command, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(command.StudentId);
        if (student is null)
            return SubmitEnrollmentRequestErrors.StudentNotFound;

        var center = await _centerRepository.GetByIdAsync(command.CenterId);
        if (center is null)
            return SubmitEnrollmentRequestErrors.CenterNotFound;

        var existingRequest = await _enrollmentRepository.GetPendingRequestAsync(command.StudentId, command.CenterId);
        if (existingRequest is not null)
            return SubmitEnrollmentRequestErrors.AlreadyEnrolled;

        var requestResult = CenterEnrollmentRequest.Create(command.StudentId, command.CenterId);
        if (requestResult.IsError)
            return requestResult.Errors;

        await _enrollmentRepository.AddAsync(requestResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return requestResult.Value.Id;
    }
}