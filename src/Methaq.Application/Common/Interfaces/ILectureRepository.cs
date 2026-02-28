using Methaq.Domain.Lectures;

namespace Methaq.Application.Common.Interfaces;

public interface ILectureRepository
{
    Task<Lecture?> GetByIdAsync(Guid id);
    Task<Lecture?> GetByIdWithDetailsAsync(Guid id);
    Task<List<Lecture>> GetBySectionIdAsync(Guid sectionId);
    Task AddAsync(Lecture lecture, CancellationToken cancellationToken);
    Task<Lecture?> GetByIdWithSectionAsync(Guid id);
}