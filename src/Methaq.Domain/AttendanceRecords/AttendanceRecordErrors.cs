using ErrorOr;
using System;

namespace Methaq.Domain.AttendanceRecords;

public static class AttendanceRecordErrors
{
    public static readonly Error StudentIdRequired = Error.Validation(
        code: "Attendance.StudentId",
        description: "Student ID is required.");

    public static readonly Error SectionIdRequired = Error.Validation(
        code: "Attendance.SectionId",
        description: "Section ID is required.");

    public static readonly Error DateCannotBeInFuture = Error.Validation(
        code: "Attendance.Date",
        description: "Attendance date cannot be in the future.");
}
