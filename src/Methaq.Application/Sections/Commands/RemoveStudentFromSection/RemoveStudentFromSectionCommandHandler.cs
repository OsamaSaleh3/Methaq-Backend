using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Notifications.enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Methaq.Application.Sections.Commands.RemoveStudentFromSection;

public class RemoveStudentFromSectionCommandHandler : IRequestHandler<RemoveStudentFromSectionCommand, ErrorOr<Success>>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly INotificationService _notificationService;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveStudentFromSectionCommandHandler(ISectionRepository sectionRepository, IStudentRepository studentRepository, IGroupChatRepository groupChatRepository, IUnitOfWork unitOfWork, INotificationService notificationService)
    {
        _sectionRepository = sectionRepository;
        _studentRepository = studentRepository;
        _groupChatRepository = groupChatRepository;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<ErrorOr<Success>> Handle(RemoveStudentFromSectionCommand request, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdWithStudentsAsync(request.SectionId);
        if (section is null)
            return RemoveStudentFromSectionErrors.SectionNotFound;

        var student = await _studentRepository.GetByIdWithUserAsync(request.StudentId,cancellationToken);
        if (student is null)
            return RemoveStudentFromSectionErrors.StudentNotFound;

        var result = section.RemoveStudent(request.StudentId);
        if (result.IsError)
            return result.Errors;

        var chat = await _groupChatRepository.GetBySectionIdAsync(request.SectionId);
        if (chat is not null)
        {
            var removeMemberResult = chat.RemoveMember(student.User.Id);
            if (removeMemberResult.IsError)
                return removeMemberResult.Errors;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendAsync(
           student.UserId,
           "تم إزالتك من الحلقة",
           $"تم إزالتك من حلقة {section.Name}",
           NotificationType.RemovedFromSection,
           section.Id);

        return Result.Success;
    }
}
