using Methaq.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Common.Interfaces
{
    public interface ISectionRepository
    {
        Task<Section?> GetByIdAsync(Guid id);
        Task<Section?> GetByIdWithDetailsAsync(Guid id);
        Task<Section?> GetByIdWithStudentsAsync(Guid id);
        Task<List<Section>> GetByCenterIdAsync(Guid centerId);
        Task AddAsync(Section section, CancellationToken cancellationToken);
    }
}
