using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.GroupChats;
using Methaq.Domain.Sections;

namespace Methaq.Application.Sections.Commands.CreateSection;

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, ErrorOr<Guid>>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IQuranCenterRepository _centerRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSectionCommandHandler(
        ISectionRepository sectionRepository,
        IQuranCenterRepository centerRepository,
        IEmployeeRepository employeeRepository,
        IGroupChatRepository groupChatRepository,
        IUnitOfWork unitOfWork)
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
            schedule);

        if (sectionResult.IsError)
            return sectionResult.Errors;

        var section = sectionResult.Value;

        var chatResult = GroupChat.Create(
            section.Name + " Chat",
            section.Id);

        if (chatResult.IsError)
            return chatResult.Errors;

        var chat = chatResult.Value;

        var addSupervisorResult = chat.AddMember(supervisor.User);
        if (addSupervisorResult.IsError)
            return addSupervisorResult.Errors;

        await _sectionRepository.AddAsync(section, cancellationToken);
        await _groupChatRepository.AddAsync(chat);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return section.Id;
    }
}