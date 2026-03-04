using ErrorOr;
using MediatR;
using Methaq.Application.Common.Emails;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.CenterEnrollmentRequests.enums;

namespace Methaq.Application.EnrollmentRequests.Commands.ApproveEnrollmentRequest;

public class ApproveEnrollmentRequestCommandHandler : IRequestHandler<ApproveEnrollmentRequestCommand, ErrorOr<Success>>
{
    private readonly IEnrollmentRequestRepository _enrollmentRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public ApproveEnrollmentRequestCommandHandler(IEnrollmentRequestRepository enrollmentRepository, IEmailService emailService, IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Success>> Handle(ApproveEnrollmentRequestCommand request, CancellationToken cancellationToken)
    {
        var requestResult = await _enrollmentRepository.GetByIdWithDetailsAsync(request.RequestId);
        if (requestResult is null)
            return ApproveEnrollmentRequestErrors.RequestNotFound;

        if(requestResult.Status!= EnrollmentRequestStatus.Pending)
            return ApproveEnrollmentRequestErrors.AlreadyProcessed;

        var result =requestResult.Approve();
        if(result.IsError)
            return result.Errors;

        var assignResult = requestResult.Student.AssignToCenter(requestResult.CenterId);
        if (assignResult.IsError)
            return assignResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _emailService.SendEmailAsync(
           requestResult.Student.User.Email!,
           EmailTemplates.EnrollmentApproved(),
           EmailTemplates.EnrollmentApproved(requestResult.Student.User.FullName, requestResult.Center.Name));

        return Result.Success;
    }
}
