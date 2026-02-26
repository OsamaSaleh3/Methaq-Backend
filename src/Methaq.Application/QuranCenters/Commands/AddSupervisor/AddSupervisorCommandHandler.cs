using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.QuranCenters.Commands.AddSupervisor;

public class AddSupervisorCommandHandler : IRequestHandler<AddSupervisorCommand, ErrorOr<Success>>
{
    private readonly IQuranCenterRepository _centerRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddSupervisorCommandHandler(IQuranCenterRepository centerRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _centerRepository = centerRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Success>> Handle(AddSupervisorCommand request, CancellationToken cancellationToken)
    {
        var center = await _centerRepository.GetByIdAsync(request.CenterId);
        if(center is null)
            return AddSupervisorErrors.CenterNotFound;

        var employee = await _employeeRepository.GetByIdAsync(request.SupervisorId);
        if(employee is null)
            return AddSupervisorErrors.SupervisorNotFound;

        if(!employee.CanBeSupervisor())
            return AddSupervisorErrors.SupervisorNotActive;

        center.AddSupervisor(employee);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
