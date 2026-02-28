namespace Methaq.Contracts.AttendanceRecords;

public record RecordAttendanceRequest(
    Guid StudentId,
    string Status,
    string? ExcuseReason,
    string? Notes
);