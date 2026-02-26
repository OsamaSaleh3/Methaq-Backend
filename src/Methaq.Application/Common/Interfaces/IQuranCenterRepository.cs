using Methaq.Domain.QuranCenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Common.Interfaces
{
    public interface IQuranCenterRepository
    {
        Task<QuranCenter?> GetByIdAsync(Guid id);
        Task<QuranCenter?> GetByIdWithDetailsAsync(Guid id);
        Task AddAsync(QuranCenter center, CancellationToken cancellationToken);
        Task<List<QuranCenter>> GetAllAsync();
        Task<List<QuranCenter>> GetAllWithDetailsAsync();

    }
}
