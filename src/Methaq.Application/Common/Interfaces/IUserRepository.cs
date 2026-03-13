using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Employees;
using Methaq.Domain.Students;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<bool> IsEmailExistsAsync(string email);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task AddToRoleAsync(ApplicationUser user, string role);
        Task AddStudentAsync(Student student, CancellationToken cancellationToken);
        Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user);
        Task UpdateAsync(ApplicationUser user);
        Task<List<(ApplicationUser User, string Role)>> GetPendingAccountsAsync();

        Task<ApplicationUser?> GetUserProfileAsync(string userId);

        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);

        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);


    }
}
