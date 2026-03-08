using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.GroupChats;
using Methaq.Domain.Sections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Methaq.Application.Sections.Commands.CreateSection;

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, ErrorOr<Guid>>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IQuranCenterRepository _centerRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSectionCommandHandler(ISectionRepository sectionRepository, IQuranCenterRepository centerRepository, IEmployeeRepository employeeRepository, IGroupChatRepository groupChatRepository, IUnitOfWork unitOfWork)
    {
        _sectionRepository = sectionRepository;
        _centerRepository = centerRepository;
        _employeeRepository = employeeRepository;
        _groupChatRepository = groupChatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        var center = await _centerRepository.GetByIdAsync(request.CenterId);
        if (center is null)
            return CreateSectionErrors.CenterNotFound;

        var supervisor = await _employeeRepository.GetByIdAsync(request.SupervisorId);
        if (supervisor is null)
            return CreateSectionErrors.SupervisorNotFound;

        if (!supervisor.CanBeSupervisor())
            return CreateSectionErrors.SupervisorNotActive;

        if (supervisor.CenterId != request.CenterId)
            return CreateSectionErrors.SupervisorNotInCenter;

        var schedule = new SectionSchedule(
           request.ScheduleDays,
           request.StartTime,
           request.EndTime);

        var sectionResult = Section.Create(
            request.Name,
            request.AcademicLevel,
            request.CenterId,
            request.SupervisorId,
            schedule
            );
        if(sectionResult.IsError)
            return sectionResult.Errors;

        var section= sectionResult.Value;

        var ChatResult = GroupChat.Create(
            section.Name+" Chat",
             section.Id
            );
        if (ChatResult.IsError)
            return ChatResult.Errors;

        var chat=ChatResult.Value;

        var supervisorUser = supervisor.User;
        var addSupervisorResult = chat.AddMember(supervisorUser);
        if (addSupervisorResult.IsError)
            return addSupervisorResult.Errors;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _sectionRepository.AddAsync(section,cancellationToken);
            await _groupChatRepository.AddAsync(chat);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            return CreateSectionErrors.CreateFailed;
        }
        return section.Id;
    }
}
