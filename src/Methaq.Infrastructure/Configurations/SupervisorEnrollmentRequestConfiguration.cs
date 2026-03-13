using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Methaq.Domain.SupervisorEnrollmentRequests;

namespace Methaq.Infrastructure.Persistence.Configurations;

public class SupervisorEnrollmentRequestConfiguration : IEntityTypeConfiguration<SupervisorEnrollmentRequest>
{
    public void Configure(EntityTypeBuilder<SupervisorEnrollmentRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Center)
            .WithMany()
            .HasForeignKey(x => x.CenterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Status)
            .HasConversion<string>();

        builder.Property(x => x.RejectionReason)
            .HasMaxLength(500);
    }
}