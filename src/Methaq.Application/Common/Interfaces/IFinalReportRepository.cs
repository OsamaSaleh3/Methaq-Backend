using Methaq.Domain.FinalReports;

namespace Methaq.Application.Common.Interfaces;

public interface IFinalReportRepository
{
    Task<FinalReport?> GetByIdAsync(Guid id);
    Task<FinalReport?> GetByIdWithStudentsAsync(Guid id);
    Task<FinalReport?> GetBySectionIdAsync(Guid sectionId);
    Task<FinalReport?> GetBySectionIdWithDetailsAsync(Guid sectionId);
    Task AddAsync(FinalReport report);
}