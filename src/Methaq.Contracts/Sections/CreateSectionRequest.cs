namespace Methaq.Contracts.Sections;
public record CreateSectionRequest(
    string Name,
    int AcademicLevel,
    Guid CenterId,
    Guid SupervisorId,
    List<DayOfWeek> ScheduleDays,
    string StartTime,
    string EndTime
);
