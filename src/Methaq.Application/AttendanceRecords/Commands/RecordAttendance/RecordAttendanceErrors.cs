using ErrorOr;

namespace Methaq.Application.AttendanceRecords.Commands.RecordAttendance;

public static class RecordAttendanceErrors
{
    public static readonly Error LectureNotFound = Error.NotFound(
        code: "Attendance.LectureNotFound",
        description: "Lecture not found.");

    public static readonly Error LectureCancelled = Error.Conflict(
        code: "Attendance.LectureCancelled",
        description: "Cannot record attendance for a cancelled lecture.");

    public static readonly Error StudentNotInSection = Error.Conflict(
        code: "Attendance.StudentNotInSection",
        description: "Student is not enrolled in this section.");

    public static readonly Error AlreadyRecorded = Error.Conflict(
        code: "Attendance.AlreadyRecorded",
        description: "Attendance already recorded for this student in this lecture.");
}