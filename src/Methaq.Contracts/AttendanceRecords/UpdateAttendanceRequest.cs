
namespace Methaq.Contracts.AttendanceRecords;

public record UpdateAttendanceRequest(
    string Status,
    string? ExcuseReason,
    string? Notes
);