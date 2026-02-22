using Methaq.Domain.QuranCenters;
using Methaq.Domain.QuranCenters.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class QuranCenterConfiguration : IEntityTypeConfiguration<QuranCenter>
{
    public void Configure(EntityTypeBuilder<QuranCenter> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.Location)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(c => c.ManagerId)
            .IsRequired();

        builder.HasOne(c => c.Manager)
            .WithOne()
            .HasForeignKey<QuranCenter>(c => c.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Sections)
            .WithOne(s => s.Center)
            .HasForeignKey(s => s.CenterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Supervisors)
            .WithMany()
            .UsingEntity(j => j.ToTable("CenterSupervisors"));

        builder.HasMany(c => c.EnrollmentRequests)
            .WithOne(r => r.Center)
            .HasForeignKey(r => r.CenterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
