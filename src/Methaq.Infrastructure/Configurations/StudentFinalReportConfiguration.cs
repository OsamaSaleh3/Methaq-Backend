using Methaq.Domain.FinalReports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class StudentFinalReportConfiguration : IEntityTypeConfiguration<StudentFinalReport>
{
    public void Configure(EntityTypeBuilder<StudentFinalReport> builder)
    {
        builder.HasKey(r => new { r.StudentId, r.FinalReportId });


        builder.Property(r => r.MemorizationScore)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(r => r.AttendanceScore)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(r => r.ParticipationScore)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(r => r.BehaviorScore)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(r => r.SupervisorNotes)
            .HasMaxLength(500);

        builder.Ignore(r => r.TotalScore);

        builder.HasOne(r => r.Student)
            .WithMany()
            .HasForeignKey(r => r.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.FinalReport)
        .WithMany(f => f.StudentReports)
        .HasForeignKey(r => r.FinalReportId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
