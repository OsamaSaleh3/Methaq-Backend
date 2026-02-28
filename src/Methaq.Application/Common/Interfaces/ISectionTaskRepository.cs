using Methaq.Domain.SectionTasks;

namespace Methaq.Application.Common.Interfaces;

public interface ISectionTaskRepository
{
    Task<SectionTask?> GetByIdAsync(Guid id);
    Task<List<SectionTask>> GetByLectureIdAsync(Guid lectureId);
    Task<List<StudentTaskEvaluation>> GetEvaluationsByStudentIdAsync(Guid studentId);
    Task AddAsync(SectionTask task);
}