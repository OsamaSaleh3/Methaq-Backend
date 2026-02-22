using Methaq.Domain.AttendanceRecords;
using Methaq.Domain.AttendanceRecords.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class AttendanceRecordConfiguration : IEntityTypeConfiguration<AttendanceRecord>
{
    public void Configure(EntityTypeBuilder<AttendanceRecord> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.StudentId)
            .IsRequired();

        builder.Property(a => a.LectureId)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(a => a.ExcuseReason)
            .HasMaxLength(300);

        builder.Property(a => a.Notes)
            .HasMaxLength(300);

        builder.HasOne(a => a.Student)
            .WithMany()
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
