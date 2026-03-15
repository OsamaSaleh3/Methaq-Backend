using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Employees;
using Methaq.Domain.Notifications.enums;
using Methaq.Domain.SupervisorEnrollmentRequests;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.ApproveRequest;

public class ApproveRequestCommandHandler : IRequestHandler<ApproveRequestCommand, ErrorOr<Success>>
{
    private readonly ISupervisorEnrollmentRequestRepository _requestRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;

    public ApproveRequestCommandHandler(ISupervisorEnrollmentRequestRepository requestRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, INotificationService notificationService)
    {
        _requestRepository = requestRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<ErrorOr<Success>> Handle(ApproveRequestCommand request, CancellationToken cancellationToken)
    {
        var enrollmentRequest = await _requestRepository.GetByIdWithDetailsAsync(request.RequestId, cancellationToken);
        if (enrollmentRequest is null)
            return SupervisorEnrollmentRequestErrors.NotFound;

        var manager = await _employeeRepository.GetByUserIdAsync(request.UserId);
        if (manager is null || manager.CenterId != enrollmentRequest.CenterId || !manager.IsManager())
            return SupervisorEnrollmentRequestErrors.NotCenterManager;

        var approveResult = enrollmentRequest.Approve();
        if (approveResult.IsError)
            return approveResult.Errors;

        var assignResult = enrollmentRequest.Employee.AssignToCenter(enrollmentRequest.CenterId);
        if (assignResult.IsError)
            return assignResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendAsync(
            enrollmentRequest.Employee.UserId,
            "تمت الموافقة على طلب التحاقك",
            $"تمت الموافقة على طلب التحاقك بمركز {enrollmentRequest.Center.Name}",
            NotificationType.SupervisorRequestApproved,
            enrollmentRequest.CenterId);

        return Result.Success;
    }
}