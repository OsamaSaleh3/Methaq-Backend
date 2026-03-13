using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Employees;
using Methaq.Domain.QuranCenters;
using Methaq.Domain.SupervisorEnrollmentRequests;

namespace Methaq.Application.SupervisorEnrollmentRequests.Commands.RequestToJoinCenter;

public class RequestToJoinCenterCommandHandler : IRequestHandler<RequestToJoinCenterCommand, ErrorOr<Guid>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IQuranCenterRepository _centerRepository;
    private readonly ISupervisorEnrollmentRequestRepository _requestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RequestToJoinCenterCommandHandler(IEmployeeRepository employeeRepository, IQuranCenterRepository centerRepository, ISupervisorEnrollmentRequestRepository requestRepository, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _centerRepository = centerRepository;
        _requestRepository = requestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(RequestToJoinCenterCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByUserIdAsync(request.UserId);
        if (employee is null)
            return RequestToJoinCenterErrors.EmployeeNotFound;

        if (employee.CenterId is not null)
            return SupervisorEnrollmentRequestErrors.AlreadyInCenter;

        var center = await _centerRepository.GetByIdAsync(request.CenterId);
        if (center is null)
            return RequestToJoinCenterErrors.CenterNotFound;

        var existingRequest = await _requestRepository.GetPendingByEmployeeAndCenterAsync(
            employee.Id,
            request.CenterId,
            cancellationToken);

        if (existingRequest is not null)
            return SupervisorEnrollmentRequestErrors.AlreadyPending;

        var enrollmentRequest = SupervisorEnrollmentRequest.Create(employee.Id, request.CenterId);
        if (enrollmentRequest.IsError)
            return enrollmentRequest.Errors;

        await _requestRepository.AddAsync(enrollmentRequest.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return enrollmentRequest.Value.Id;
    }
}