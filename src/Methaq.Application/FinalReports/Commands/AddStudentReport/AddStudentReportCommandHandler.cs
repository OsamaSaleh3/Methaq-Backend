using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.AttendanceRecords.enums;
using Methaq.Domain.FinalReports;
using Methaq.Domain.SectionTasks.enums;

namespace Methaq.Application.FinalReports.Commands.AddStudentReport;

public class AddStudentReportCommandHandler : IRequestHandler<AddStudentReportCommand, ErrorOr<Success>>
{
    private readonly IFinalReportRepository _finalReportRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IAttendanceRecordRepository _attendanceRepository;
    private readonly ISectionTaskRepository _sectionTaskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddStudentReportCommandHandler(IFinalReportRepository finalReportRepository, ISectionRepository sectionRepository, IStudentRepository studentRepository, IAttendanceRecordRepository attendanceRepository, ISectionTaskRepository sectionTaskRepository, IUnitOfWork unitOfWork)
    {
        _finalReportRepository = finalReportRepository;
        _sectionRepository = sectionRepository;
        _studentRepository = studentRepository;
        _attendanceRepository = attendanceRepository;
        _sectionTaskRepository = sectionTaskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(AddStudentReportCommand request, CancellationToken cancellationToken)
    {
        var report = await _finalReportRepository.GetByIdAsync(request.FinalReportId);
        if (report is null)
            return AddStudentReportErrors.ReportNotFound;

        var student = await _studentRepository.GetByIdAsync(request.StudentId,cancellationToken);
        if (student is null)
            return AddStudentReportErrors.StudentNotFound;

        var section = await _sectionRepository.GetByIdWithStudentsAsync(report.SectionId);
        var studentInSection = section!.Students.Any(s => s.Id == request.StudentId);
        if (!studentInSection)
            return AddStudentReportErrors.StudentNotInSection;

        var attendenceRecords = await _attendanceRepository.GetByStudentIdAsync(request.StudentId);
        var attencenceInSection = attendenceRecords
            .Where(a => a.Lecture.SectionId == report.SectionId)
            .ToList();

        var totalAttendance = attencenceInSection
            .Count(a=>a.Status==AttendanceStatus.Present);

        var totalLectures = attencenceInSection.Count;

        var attendanceScore = totalLectures > 0
            ? (decimal)totalAttendance / totalLectures * 100: 0;

        var evaluation=await _sectionTaskRepository
            .GetEvaluationsByStudentIdAsync(request.StudentId);
        var evaluationInSection = evaluation
            .Where(e => e.SectionTask.SectionId == report.SectionId)
            .ToList();

        var averageEvaluation = evaluationInSection.Any() ?
            evaluationInSection.Average(e => e.AchievedMark) 
            : 0;

        var memorizationScore = evaluationInSection
            .Where(e => e.SectionTask.Types == TaskTypes.Memorization)
            .Any() ?
            evaluationInSection.Where(e => e.SectionTask.Types == TaskTypes.Memorization)
            .Average(e => e.AchievedMark)
            :0;

        var studenReport = new StudentFinalReport(
            request.StudentId,
            request.FinalReportId,
            memorizationScore,
            attendanceScore,
            request.ParticipationScore,
            request.BehaviorScore,
            request.SupervisorNotes
            );

        var addResult=report.AddStudentReport(studenReport);
        if (addResult.IsError)
            return addResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
