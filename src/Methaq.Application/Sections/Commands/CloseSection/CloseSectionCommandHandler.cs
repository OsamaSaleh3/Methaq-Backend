using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Notifications.enums;
using Methaq.Domain.Students;

namespace Methaq.Application.Sections.Commands.CloseSection;

public class CloseSectionCommandHandler : IRequestHandler<CloseSectionCommand, ErrorOr<Success>>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly INotificationService _notificationService;
    private readonly IUnitOfWork _unitOfWork;

    public CloseSectionCommandHandler(
        ISectionRepository sectionRepository,
        IUnitOfWork unitOfWork,
        INotificationService notificationService)
    {
        _sectionRepository = sectionRepository;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<ErrorOr<Success>> Handle(CloseSectionCommand command, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdWithDetailsAsync(command.SectionId);
        if (section is null)
            return CloseSectionErrors.SectionNotFound;

        var result = section.Close();
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var student in section.Students)
        {
            await _notificationService.SendAsync(
                student.UserId,
                "تم إغلاق الحلقة",
                $"تم إغلاق حلقة {section.Name}",
                NotificationType.SectionClosed,
                section.Id);
        }

        return Result.Success;
    }
}