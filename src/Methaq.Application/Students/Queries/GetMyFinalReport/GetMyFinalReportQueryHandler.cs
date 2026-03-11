using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.Students;

namespace Methaq.Application.UseCases.Students.Queries.GetMyFinalReport;

public class GetMyFinalReportQueryHandler : IRequestHandler<GetMyFinalReportQuery, ErrorOr<StudentFinalReportResponse>>
{
    private readonly IStudentRepository _studentRepository;

    public GetMyFinalReportQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<ErrorOr<StudentFinalReportResponse>> Handle(GetMyFinalReportQuery request, CancellationToken cancellationToken)
    {
        var report = await _studentRepository.GetMyFinalReportAsync(request.UserId, cancellationToken);
        if (report is null)
            return StudentErrors.FinalReportNotFound;

        return new StudentFinalReportResponse(
            report.FinalReportId,
            report.MemorizationScore,
            report.AttendanceScore,
            report.ParticipationScore,
            report.BehaviorScore,
            report.TotalScore,
            report.SupervisorNotes);
    }
}