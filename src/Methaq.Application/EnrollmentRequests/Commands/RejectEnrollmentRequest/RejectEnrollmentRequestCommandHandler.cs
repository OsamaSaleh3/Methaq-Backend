using ErrorOr;
using MediatR;
using Methaq.Application.Common.Emails;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.CenterEnrollmentRequests.enums;
using Methaq.Domain.Notifications.enums;

namespace Methaq.Application.EnrollmentRequests.Commands.RejectEnrollmentRequest;

public class RejectEnrollmentRequestCommandHandler : IRequestHandler<RejectEnrollmentRequestCommand, ErrorOr<Success>>
{
    private readonly IEnrollmentRequestRepository _enrollmentRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;

    public RejectEnrollmentRequestCommandHandler(IEnrollmentRequestRepository enrollmentRepository, IEmailService emailService, IUnitOfWork unitOfWork, INotificationService notificationService)
    {
        _enrollmentRepository = enrollmentRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<ErrorOr<Success>> Handle(RejectEnrollmentRequestCommand command, CancellationToken cancellationToken)
    {
        var request = await _enrollmentRepository.GetByIdWithDetailsAsync(command.RequestId);
        if (request is null)
            return RejectEnrollmentRequestErrors.RequestNotFound;

        if (request.Status!= EnrollmentRequestStatus.Pending)
            return RejectEnrollmentRequestErrors.AlreadyProcessed;

        var result = request.Reject(command.Reason);
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendAsync(
            request.Student.UserId,
            "تم رفض طلب التحاقك",
            $"نأسف، تم رفض طلب انضمامك لمركز {request.Center.Name}",
            NotificationType.EnrollmentRejected,
            request.CenterId);

        await _emailService.SendEmailAsync(
            request.Student.User.Email!,
            EmailTemplates.EnrollmentRejected(),
            EmailTemplates.EnrollmentRejected(request.Student.User.FullName, request.Center.Name, command.Reason));

        return Result.Success;
    }
}