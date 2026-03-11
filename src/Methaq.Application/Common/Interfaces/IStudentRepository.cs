using Methaq.Domain.FinalReports;
using Methaq.Domain.Lectures;
using Methaq.Domain.SectionTasks;
using Methaq.Domain.Students;

namespace Methaq.Application.Common.Interfaces;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Student?> GetByIdWithUserAsync(Guid studentId, CancellationToken cancellationToken);
    Task<List<Student>> GetAllWithUserAsync(CancellationToken cancellationToken);
    Task<List<Student>> GetByCenterAsync(Guid centerId, CancellationToken cancellationToken);
    Task<Student?> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<Student?> GetByUserIdWithSectionAsync(string userId, CancellationToken cancellationToken);
    Task<StudentFinalReport?> GetMyFinalReportAsync(string userId, CancellationToken cancellationToken);
    Task<List<Lecture>> GetMyLecturesAsync(Guid studentId, CancellationToken cancellationToken);
    Task<List<SectionTask>> GetMyTasksAsync(Guid studentId, CancellationToken cancellationToken);

}