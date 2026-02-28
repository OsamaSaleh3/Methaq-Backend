using ErrorOr;
using MediatR;

namespace Methaq.Application.FinalReports.Queries.GetFinalReportBySection;

public record GetFinalReportBySectionQuery(Guid SectionId)
    : IRequest<ErrorOr<FinalReportResponse>>;

public record FinalReportResponse(
    Guid Id,
    Guid SectionId,
    string SectionName,
    DateTime GeneratedAt,
    string? GeneralNotes,
    bool EmailSentToStudents,
    DateTime? EmailSentAt,
    List<StudentFinalReportResponse> StudentReports
);

public record StudentFinalReportResponse(
    Guid StudentId,
    string StudentName,
    decimal MemorizationScore,
    decimal AttendanceScore,
    decimal ParticipationScore,
    decimal BehaviorScore,
    decimal TotalScore,
    string? SupervisorNotes
);