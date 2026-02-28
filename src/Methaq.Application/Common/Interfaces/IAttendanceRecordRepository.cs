using Methaq.Domain.AttendanceRecords;

namespace Methaq.Application.Common.Interfaces;

public interface IAttendanceRecordRepository
{
    Task<AttendanceRecord?> GetByIdAsync(Guid id);
    Task<List<AttendanceRecord>> GetByLectureIdAsync(Guid lectureId);
    Task<List<AttendanceRecord>> GetByStudentIdAsync(Guid studentId);
    Task<bool> ExistsAsync(Guid lectureId, Guid studentId);
    Task AddAsync(AttendanceRecord record);
}