using ErrorOr;

namespace Methaq.Application.AttendanceRecords.Commands.UpdateAttendance;

public static class UpdateAttendanceErrors
{
    public static readonly Error NotFound = Error.NotFound(
        code: "Attendance.NotFound",
        description: "Attendance record not found.");
}