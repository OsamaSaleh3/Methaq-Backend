using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.FinalReports.Queries.GetFinalReportBySection;

public class GetFinalReportBySectionQueryHandler
    : IRequestHandler<GetFinalReportBySectionQuery, ErrorOr<FinalReportResponse>>
{
    private readonly IFinalReportRepository _finalReportRepository;

    public GetFinalReportBySectionQueryHandler(IFinalReportRepository finalReportRepository)
    {
        _finalReportRepository = finalReportRepository;
    }

    public async Task<ErrorOr<FinalReportResponse>> Handle(
        GetFinalReportBySectionQuery query,
        CancellationToken cancellationToken)
    {
        var report = await _finalReportRepository.GetBySectionIdWithDetailsAsync(query.SectionId);
        if (report is null)
            return Error.NotFound("FinalReport.NotFound", "Final report not found for this section.");

        return new FinalReportResponse(
            report.Id,
            report.SectionId,
            report.Section.Name,
            report.GeneratedAt,
            report.GeneralNotes,
            report.EmailSentToStudents,
            report.EmailSentAt,
            report.StudentReports.Select(sr => new StudentFinalReportResponse(
                sr.StudentId,
                sr.Student.User.FullName,
                sr.MemorizationScore,
                sr.AttendanceScore,
                sr.ParticipationScore,
                sr.BehaviorScore,
                sr.TotalScore,
                sr.SupervisorNotes
            )).ToList()
        );
    }
}