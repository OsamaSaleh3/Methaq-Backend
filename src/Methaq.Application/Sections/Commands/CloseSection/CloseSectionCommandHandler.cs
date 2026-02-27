using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Sections.Commands.CloseSection;

public class CloseSectionCommandHandler : IRequestHandler<CloseSectionCommand, ErrorOr<Success>>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CloseSectionCommandHandler(
        ISectionRepository sectionRepository,
        IUnitOfWork unitOfWork)
    {
        _sectionRepository = sectionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(CloseSectionCommand command, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdAsync(command.SectionId);
        if (section is null)
            return CloseSectionErrors.SectionNotFound;

        var result = section.Close();
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}