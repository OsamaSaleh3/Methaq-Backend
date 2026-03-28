using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.QuranCenters.Commands.TransferManagement;

public class TransferManagementCommandHandler : IRequestHandler<TransferManagementCommand, ErrorOr<Success>>
{
    private readonly IQuranCenterRepository _centerRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TransferManagementCommandHandler(IQuranCenterRepository centerRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _centerRepository = centerRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(TransferManagementCommand request, CancellationToken cancellationToken)
    {
        var center =await _centerRepository.GetByIdAsync(request.CenterId);
        if (center is null)
            return TransferManagementErrors.CenterNotFound;

        var newManager = await _employeeRepository.GetByIdAsync(request.NewManagerId);
        if (newManager is null)
            return TransferManagementErrors.NewManagerNotFound;

        var oldManager = await _employeeRepository.GetByIdAsync(center.ManagerId);
        if (oldManager is null)
            return TransferManagementErrors.OldManagerNotFound;

        if (!newManager.CanBeSupervisor())
        {
            return TransferManagementErrors.NewManagerNotActive;
        }

        
        var demoteResult = oldManager.DemoteToSupervisor();
        if (demoteResult.IsError)
            return demoteResult.Errors;

        var promoteResult = newManager.PromoteToManager(request.CenterId);
        if (promoteResult.IsError)
            return promoteResult.Errors;


        var transferResult=center.TransferManagement(request.NewManagerId);
        if(transferResult.IsError)
            return transferResult.Errors;

        await _unitOfWork.SaveChangesAsync();
        return Result.Success;

    }
}
