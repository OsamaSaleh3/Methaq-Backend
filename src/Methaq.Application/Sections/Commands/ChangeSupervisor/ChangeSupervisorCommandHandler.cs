using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Methaq.Application.Sections.Commands.ChangeSupervisor;

public class ChangeSupervisorCommandHandler : IRequestHandler<ChangeSupervisorCommand, ErrorOr<Success>>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeSupervisorCommandHandler(ISectionRepository sectionRepository, IEmployeeRepository employeeRepository, IGroupChatRepository groupChatRepository, IUnitOfWork unitOfWork)
    {
        _sectionRepository = sectionRepository;
        _employeeRepository = employeeRepository;
        _groupChatRepository = groupChatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(ChangeSupervisorCommand request, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdAsync(request.SectionId);
        if (section is null)
            return ChangeSupervisorErrors.SectionNotFound;

        var newSupervisor = await _employeeRepository.GetByIdWithUserAsync(request.NewSupervisorId);
        if (newSupervisor is null)
            return ChangeSupervisorErrors.SupervisorNotFound;

        if (!newSupervisor.CanBeSupervisor())
            return ChangeSupervisorErrors.SupervisorNotActive;

        if (newSupervisor.CenterId != section.CenterId)
            return ChangeSupervisorErrors.SupervisorNotInCenter;

        var chat = await _groupChatRepository.GetBySectionIdAsync(request.SectionId);
        chat?.RemoveMember(section.Supervisor.User.Id);

        var result = section.ChangeSupervisor(request.NewSupervisorId);
        if (result.IsError)
            return result.Errors;
        
        chat?.AddMember(newSupervisor.User);

        await _unitOfWork.SaveChangesAsync();
        return Result.Success;
    }
}
