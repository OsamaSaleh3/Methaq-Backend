using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Employees;
using Methaq.Domain.Employees.enums;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<List<Employee>> GetAvailableSupervisorsAsync()
        {
            return await _context.Employees
                .Include(e => e.User)
                .Where(e => e.CenterId == null
                    && e.EmploymentStatus == EmploymentStatus.Active
                    && e.Role == EmployeeRole.Supervisor)
                .ToListAsync();
        }
    }
}
