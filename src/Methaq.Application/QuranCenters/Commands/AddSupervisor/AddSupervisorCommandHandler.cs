using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        if (employee is null)
            return AddSupervisorErrors.SupervisorNotFound;

        if (employee.CenterId is not null)
            return AddSupervisorErrors.SupervisorAlreadyAssigned;

        if (!employee.CanBeSupervisor())
            return AddSupervisorErrors.SupervisorNotActive;

        var assignResult = employee.AssignToCenter(request.CenterId);
        if (assignResult.IsError)
            return assignResult.Errors;

        var result = center.AddSupervisor(employee);
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
