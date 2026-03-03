namespace Methaq.Contracts.Sections;
public record CreateSectionRequest(
    string Name,
    int AcademicLevel,
    Guid CenterId,
    Guid SupervisorId,
    List<string> ScheduleDays,
    string StartTime,
    string EndTime
);
