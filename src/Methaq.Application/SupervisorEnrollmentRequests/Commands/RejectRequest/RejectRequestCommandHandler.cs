using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Employees;
using Methaq.Domain.Notifications.enums;
using Methaq.Domain.SupervisorEnrollmentRequests;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.RejectRequest;

public class RejectRequestCommandHandler : IRequestHandler<RejectRequestCommand, ErrorOr<Success>>
{
    private readonly ISupervisorEnrollmentRequestRepository _requestRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;

    public RejectRequestCommandHandler(ISupervisorEnrollmentRequestRepository requestRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, INotificationService notificationService)
    {
        _requestRepository = requestRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<ErrorOr<Success>> Handle(RejectRequestCommand request, CancellationToken cancellationToken)
    {
        var enrollmentRequest = await _requestRepository.GetByIdWithDetailsAsync(request.RequestId, cancellationToken);
        if (enrollmentRequest is null)
            return SupervisorEnrollmentRequestErrors.NotFound;

        var manager = await _employeeRepository.GetByUserIdAsync(request.UserId);
        if (manager is null || manager.CenterId != enrollmentRequest.CenterId || !manager.IsManager())
            return SupervisorEnrollmentRequestErrors.NotCenterManager;

        var rejectResult = enrollmentRequest.Reject(request.Reason);
        if (rejectResult.IsError)
            return rejectResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendAsync(
            enrollmentRequest.Employee.UserId,
            "تم رفض طلب انضمامك للمركز",
            $"نأسف, تم رفض طلب انضمامك للمركز {enrollmentRequest.Center.Name}",
            NotificationType.SupervisorRequestRejected,
            enrollmentRequest.CenterId);


        return Result.Success;
    }
}