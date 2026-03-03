namespace Methaq.Contracts.AttendanceRecords;

public record RecordAttendanceRequest(
    Guid StudentId,
    int Status,
    string? ExcuseReason,
    string? Notes
);