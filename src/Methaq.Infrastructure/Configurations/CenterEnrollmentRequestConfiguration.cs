using Methaq.Domain.CenterEnrollmentRequests;
using Methaq.Domain.CenterEnrollmentRequests.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class CenterEnrollmentRequestConfiguration : IEntityTypeConfiguration<CenterEnrollmentRequest>
{
    public void Configure(EntityTypeBuilder<CenterEnrollmentRequest> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.StudentId)
            .IsRequired();

        builder.Property(r => r.CenterId)
            .IsRequired();

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(r => r.RejectionReason)
            .HasMaxLength(300);

        builder.Property(r => r.ReviewedAt);

        builder.HasOne(r => r.Student)
            .WithMany()
            .HasForeignKey(r => r.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
