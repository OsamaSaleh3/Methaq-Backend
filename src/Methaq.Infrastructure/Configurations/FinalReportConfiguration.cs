using Methaq.Domain.FinalReports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class FinalReportConfiguration : IEntityTypeConfiguration<FinalReport>
{
    public void Configure(EntityTypeBuilder<FinalReport> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.SectionId)
            .IsRequired();

        builder.Property(r => r.GeneratedAt)
            .IsRequired();

        builder.Property(r => r.GeneralNotes)
            .HasMaxLength(500);

        builder.Property(r => r.EmailSentToStudents)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(r => r.EmailSentAt);

        builder.HasOne(r => r.Section)
            .WithMany()
            .HasForeignKey(r => r.SectionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.StudentReports)
            .WithOne()
            .HasForeignKey(sr => sr.FinalReportId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
