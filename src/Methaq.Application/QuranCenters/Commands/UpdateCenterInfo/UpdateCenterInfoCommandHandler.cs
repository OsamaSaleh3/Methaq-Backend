using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.QuranCenters.Commands.UpdateCenterInfo;

public class UpdateCenterInfoCommandHandler : IRequestHandler<UpdateCenterInfoCommand, ErrorOr<Success>>
{
    private readonly IQuranCenterRepository _centerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCenterInfoCommandHandler(IQuranCenterRepository centerRepository, IUnitOfWork unitOfWork)
    {
        _centerRepository = centerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateCenterInfoCommand request, CancellationToken cancellationToken)
    {
        var center = await _centerRepository.GetByIdAsync(request.CenterId);
        if (center is null)
            return UpdateCenterInfoErrors.CenterNotFound;

        var updateCenterResult=center.UpdateInfo(request.Name, request.Description, request.Location, request.PhoneNumber);
        if(updateCenterResult.IsError)
            return updateCenterResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
