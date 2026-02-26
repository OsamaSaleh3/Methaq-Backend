using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.QuranCenters.Commands.RemoveSupervisor;

public class RemoveSupervisorCommandHandler : IRequestHandler<RemoveSupervisorCommand, ErrorOr<Success>>
{
    private readonly IQuranCenterRepository _centerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSupervisorCommandHandler(IQuranCenterRepository centerRepository, IUnitOfWork unitOfWork)
    {
        _centerRepository = centerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(RemoveSupervisorCommand request, CancellationToken cancellationToken)
    {
        var center =await _centerRepository.GetByIdAsync(request.CenterId);
        if (center is null) 
            return RemoveSupervisorErrors.CenterNotFound;

        var result = center.RemoveSupervisor(request.SupervisorId);
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
