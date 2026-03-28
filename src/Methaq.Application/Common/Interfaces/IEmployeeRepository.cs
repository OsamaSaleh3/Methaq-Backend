using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Employees;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Common.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(Guid id);
        Task<Employee?>GetByIdWithDetailsAsync(Guid id);
        Task<List<Employee>> GetAvailableSupervisorsAsync();
        Task<Employee?> GetByIdWithUserAsync(Guid id);
        Task<List<Employee>> GetAllWithUserAsync();
        Task<List<Employee>> GetByCenterAsync(Guid centerId, CancellationToken cancellationToken);
        Task<Employee?> GetByUserIdAsync(string userId);

    }
}
