using ErrorOr;

namespace Methaq.Domain.AttendanceRecords;

public static class AttendanceRecordErrors
{
    public static readonly Error StudentIdRequired = Error.Validation(
        code: "Attendance.StudentId",
        description: "Student ID is required.");

    public static readonly Error LectureIdRequired = Error.Validation(
        code: "Attendance.LectureId",
        description: "Lecture ID is required.");

    public static readonly Error ExcuseReasonRequired = Error.Validation(
        code: "Attendance.ExcuseReason",
        description: "Excuse reason is required when status is Excused.");
}
