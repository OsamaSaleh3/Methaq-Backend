using ErrorOr;

namespace Methaq.Domain.Lectures;

public static class LectureErrors
{
    public static readonly Error SectionIdRequired = Error.Validation(
        code: "Lecture.SectionId",
        description: "Section ID is required.");

    public static readonly Error DateCannotBeInPast = Error.Validation(
        code: "Lecture.Date",
        description: "Lecture date cannot be in the past.");

    public static readonly Error InvalidTimeRange = Error.Validation(
        code: "Lecture.TimeRange",
        description: "End time must be after start time.");

    public static readonly Error AttendanceRecordNull = Error.Validation(
        code: "Lecture.AttendanceNull",
        description: "Attendance record cannot be null.");

    public static readonly Error TaskNull = Error.Validation(
        code: "Lecture.TaskNull",
        description: "Task cannot be null.");

    public static readonly Error LectureCancelled = Error.Conflict(
        code: "Lecture.Cancelled",
        description: "Cannot modify a cancelled lecture.");

    public static readonly Error AttendanceAlreadyRecorded = Error.Conflict(
        code: "Lecture.AttendanceExists",
        description: "Attendance already recorded for this student.");

    public static readonly Error CannotStartLecture = Error.Conflict(
        code: "Lecture.CannotStart",
        description: "Only scheduled lectures can be started.");

    public static readonly Error AlreadyCompleted = Error.Conflict(
        code: "Lecture.AlreadyCompleted",
        description: "Lecture is already completed.");

    public static readonly Error CannotCancelCompleted = Error.Conflict(
        code: "Lecture.CannotCancel",
        description: "Cannot cancel a completed lecture.");
}
