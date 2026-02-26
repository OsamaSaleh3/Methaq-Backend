using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.QuranCenters.Commands.CloseCenter;

public class CloseCenterCommandHandler : IRequestHandler<CloseCenterCommand, ErrorOr<Success>>
{
    private readonly IQuranCenterRepository _centerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CloseCenterCommandHandler(
        IQuranCenterRepository centerRepository,
        IUnitOfWork unitOfWork)
    {
        _centerRepository = centerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(CloseCenterCommand command, CancellationToken cancellationToken)
    {
        var center = await _centerRepository.GetByIdAsync(command.CenterId);
        if (center is null)
            return CloseCenterErrors.CenterNotFound;

        var result = center.Close();
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}