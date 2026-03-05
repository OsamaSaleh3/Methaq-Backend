using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.QuranCenters;

namespace Methaq.Application.QuranCenters.Commands.CreateCenter;

public class CreateCenterCommandHandler : IRequestHandler<CreateCenterCommand, ErrorOr<Guid>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IQuranCenterRepository _quranCenterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCenterCommandHandler(IUnitOfWork unitOfWork, IQuranCenterRepository quranCenterRepository, IEmployeeRepository employeeRepository)
    {
        _unitOfWork = unitOfWork;
        _quranCenterRepository = quranCenterRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateCenterCommand request, CancellationToken cancellationToken)
    {
        var manager = await _employeeRepository.GetByIdAsync(request.ManagerId);
        if (manager is null)
            return CreateCenterErrors.ManagerNotFound;

        if (!manager.CanBeSupervisor())
            return CreateCenterErrors.ManagerNotActive;

        if(!manager.IsManager())
            return CreateCenterErrors.ManagerNotEligible;

        var centerResult = QuranCenter.Create(
                request.Name,
                request.Description,
                request.Location,
                request.PhoneNumber,
                request.ManagerId
            );

        if (centerResult.IsError)
            return centerResult.Errors;

        var center = centerResult.Value;

        center.AddSupervisor(manager);

        await _quranCenterRepository.AddAsync(center, cancellationToken);

        await _unitOfWork.SaveChangesAsync();

        return center.Id;

        
    }
}
