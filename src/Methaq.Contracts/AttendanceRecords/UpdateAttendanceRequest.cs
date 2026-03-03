
namespace Methaq.Contracts.AttendanceRecords;

public record UpdateAttendanceRequest(
    int Status,
    string? ExcuseReason,
    string? Notes
);