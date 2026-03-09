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
        Task<List<Employee>> GetAvailableSupervisorsAsync();
        Task<Employee?> GetByIdWithUserAsync(Guid id);
        
    }
}
