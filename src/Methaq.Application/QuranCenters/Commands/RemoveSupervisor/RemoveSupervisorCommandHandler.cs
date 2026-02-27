using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.QuranCenters.Commands.RemoveSupervisor;

public class RemoveSupervisorCommandHandler : IRequestHandler<RemoveSupervisorCommand, ErrorOr<Success>>
{
    private readonly IQuranCenterRepository _centerRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSupervisorCommandHandler(IQuranCenterRepository centerRepository, IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
    {
        _centerRepository = centerRepository;
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<Success>> Handle(RemoveSupervisorCommand request, CancellationToken cancellationToken)
    {
        var center =await _centerRepository.GetByIdAsync(request.CenterId);
        if (center is null) 
            return RemoveSupervisorErrors.CenterNotFound;

        var employee = await _employeeRepository.GetByIdAsync(request.SupervisorId);
        if(employee is null) 
            return RemoveSupervisorErrors.SupervisorNotFound;

        var result = center.RemoveSupervisor(request.SupervisorId);
        if (result.IsError)
            return result.Errors;

        employee.RemoveFromCenter();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
