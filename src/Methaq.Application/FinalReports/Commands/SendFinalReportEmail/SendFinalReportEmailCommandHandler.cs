using ErrorOr;
using MediatR;
using Methaq.Application.Common.Emails;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Notifications.enums;
using Methaq.Domain.Students;

namespace Methaq.Application.FinalReports.Commands.SendFinalReportEmail;

public class SendFinalReportEmailCommandHandler : IRequestHandler<SendFinalReportEmailCommand, ErrorOr<Success>>
{
    private readonly IFinalReportRepository _finalReportRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly INotificationService _notificationService;
    private readonly IUnitOfWork _unitOfWork;

    public SendFinalReportEmailCommandHandler(
        IFinalReportRepository finalReportRepository,
        IUserRepository userRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork,
        INotificationService notificationService)
    {
        _finalReportRepository = finalReportRepository;
        _userRepository = userRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<ErrorOr<Success>> Handle(SendFinalReportEmailCommand command, CancellationToken cancellationToken)
    {
        var report = await _finalReportRepository.GetByIdWithStudentsAsync(command.FinalReportId);
        if (report is null)
            return SendFinalReportEmailErrors.ReportNotFound;

        if (report.EmailSentToStudents)
            return SendFinalReportEmailErrors.EmailAlreadySent;

        foreach (var studentReport in report.StudentReports)
        {
            var user = await _userRepository.GetByIdAsync(studentReport.Student.UserId);
            if (user is null) continue;

            var emailTo = studentReport.Student.GuardianEmail ?? user.Email;
            if (emailTo is null) continue;

            await _emailService.SendEmailAsync(
                emailTo,
                EmailTemplates.FinalReport(),
                EmailTemplates.FinalReport(
                    user.FullName,
                    studentReport.MemorizationScore,
                    studentReport.AttendanceScore,
                    studentReport.ParticipationScore,
                    studentReport.BehaviorScore,
                    studentReport.TotalScore
                    )
                );

            await _notificationService.SendAsync(
             user.Id,
           "التقرير النهائي جاهز ",
           $" تم إصدار تقريرك النهائي، يمكنك الاطلاع عليه الآن عن طريق الايميل",
           NotificationType.FinalReportReady,
           report.Section.Id);
        }

        var markResult = report.MarkEmailSent();
        if (markResult.IsError)
            return markResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

       

        return Result.Success;
    }
}