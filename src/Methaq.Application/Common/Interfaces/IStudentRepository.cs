using Methaq.Domain.Students;

namespace Methaq.Application.Common.Interfaces;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid id);
    Task<Student?> GetByIdWithUserAsync(Guid id);

}