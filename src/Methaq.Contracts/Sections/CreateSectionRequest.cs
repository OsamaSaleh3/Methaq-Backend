namespace Methaq.Contracts.Sections;
public record CreateSectionRequest(
    string Name,
    string AcademicLevel,
    Guid CenterId,
    Guid SupervisorId,
    List<string> ScheduleDays,
    string StartTime,
    string EndTime
);
