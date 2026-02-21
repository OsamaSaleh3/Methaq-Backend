using ErrorOr;

namespace Methaq.Domain.Sections;

public static class SectionErrors
{
    public static readonly Error NameRequired = Error.Validation(
        code: "Section.Name",
        description: "Section name cannot be empty.");

    public static readonly Error CenterIdRequired = Error.Validation(
        code: "Section.CenterId",
        description: "Center ID is required.");

    public static readonly Error SupervisorIdRequired = Error.Validation(
        code: "Section.SupervisorId",
        description: "Supervisor ID is required.");

    public static readonly Error ScheduleRequired = Error.Validation(
        code: "Section.Schedule",
        description: "Section schedule is required.");

    public static readonly Error InvalidSchedule= Error.Validation(
        code: "Section.InvalidSchedule",
        description: "Schedule info invalid.");

    public static readonly Error ScheduleInvalidTime = Error.Validation(
        code: "Section.InvalidScheduleTime",
        description: "End Time Cannot be in the past");

    public static readonly Error ScheduleDaysRequired = Error.Validation(
        code: "Section.ScheduleDays",
        description: "Schedule Days are Required");

    public static readonly Error StudentNull = Error.Validation(
        code: "Section.StudentNull",
        description: "Student cannot be null.");

    public static readonly Error StudentExists = Error.Conflict(
        code: "Section.StudentExists",
        description: "Student already in this section.");

    public static readonly Error StudentNotFound = Error.NotFound(
        code: "Section.StudentNotFound",
        description: "Student not found in this section.");

    public static readonly Error SupervisorEmpty = Error.Validation(
        code: "Section.SupervisorEmpty",
        description: "Supervisor cannot be empty.");

    public static readonly Error SameSupervisor = Error.Validation(
        code: "Section.SameSupervisor",
        description: "New supervisor must be different from current.");

    public static readonly Error SectionClosed = Error.Conflict(
        code: "Section.Closed",
        description: "Cannot modify a closed section.");

    public static readonly Error AlreadyClosed = Error.Conflict(
        code: "Section.AlreadyClosed",
        description: "Section is already closed.");

    public static readonly Error LectureNull = Error.Validation(
        code: "Section.LectureNull",
        description: "Lecture cannot be null.");
}
