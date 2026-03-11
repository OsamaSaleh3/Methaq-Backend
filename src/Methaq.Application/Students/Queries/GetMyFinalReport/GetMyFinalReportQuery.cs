using ErrorOr;
using MediatR;

namespace Methaq.Application.UseCases.Students.Queries.GetMyFinalReport;

public record GetMyFinalReportQuery(string UserId) : IRequest<ErrorOr<StudentFinalReportResponse>>;

public record StudentFinalReportResponse(
    Guid FinalReportId,
    decimal MemorizationScore,
    decimal AttendanceScore,
    decimal ParticipationScore,
    decimal BehaviorScore,
    decimal TotalScore,
    string? SupervisorNotes);