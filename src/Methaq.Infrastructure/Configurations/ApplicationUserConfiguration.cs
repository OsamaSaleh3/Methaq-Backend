using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.ApplicationUsers.enums;
using Methaq.Domain.Employees;
using Methaq.Domain.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.SecondName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.ThirdName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.NationalId)
            .HasMaxLength(20);

        builder.Property(u => u.Address)
            .HasMaxLength(200);

        builder.Property(u => u.DateOfBirth)
            .IsRequired();

        builder.Property(a => a.AccountStatus)
            .IsRequired()
            .HasDefaultValue(AccountStatus.Pending)
            .HasConversion<string>();

        builder.Ignore(u => u.FullName);

        builder.HasOne(u=>u.Employee)
            .WithOne(e => e.User)
            .HasForeignKey<Employee>(e=>e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u=>u.Student)
            .WithOne(s=>s.User)
            .HasForeignKey<Student>(s=>s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
