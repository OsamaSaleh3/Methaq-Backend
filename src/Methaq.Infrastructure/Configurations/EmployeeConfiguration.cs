using Methaq.Domain.Employees;
using Methaq.Domain.Employees.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Specialization)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.IslamicQualifications)
            .HasMaxLength(200);

        builder.Property(e => e.CurrentJob)
            .HasMaxLength(100);

        builder.Property(e => e.HireDate)
            .IsRequired();

        builder.Property(e => e.Degree)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(e => e.EmploymentStatus)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(e => e.Role)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(e => e.ManagedCenterId);

        builder.HasMany(e => e.SupervisedSections)
            .WithOne(s => s.Supervisor)
            .HasForeignKey(s => s.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
