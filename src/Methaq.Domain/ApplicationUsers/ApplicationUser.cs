using Methaq.Domain.ApplicationUsers.enums;
using Methaq.Domain.Employees;
using Methaq.Domain.Students;
using Microsoft.AspNetCore.Identity;

namespace Methaq.Domain.ApplicationUsers
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string ThirdName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {SecondName} {ThirdName} {LastName}";
        public string? NationalId { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Address { get; set; }

        public AccountStatus AccountStatus { get; set; } = AccountStatus.Pending;
      
        public Employee? Employee { get; set; }
        public Student? Student { get; set; }
    }
}
