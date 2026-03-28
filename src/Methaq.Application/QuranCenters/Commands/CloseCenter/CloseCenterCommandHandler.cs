using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.CenterEnrollmentRequests.enums;
using Methaq.Domain.Sections.enums;

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
        var center = await _centerRepository.GetByIdWithDetailsAsync(command.CenterId);
        if (center is null)
            return CloseCenterErrors.CenterNotFound;

        foreach(var section in center.Sections)
        {
            if (section.Status == SectionStatus.Active)
            {
                section.Close();
            }
        }
        foreach (var request in center.EnrollmentRequests)
        {
            if (request.Status == EnrollmentRequestStatus.Pending)
            {
                request.Reject();
            }
        }

        var result = center.Close();
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}