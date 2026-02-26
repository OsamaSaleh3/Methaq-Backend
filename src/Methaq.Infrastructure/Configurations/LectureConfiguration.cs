using Methaq.Domain.Lectures;
using Methaq.Domain.Lectures.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
{
    public void Configure(EntityTypeBuilder<Lecture> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Date)
            .IsRequired();

        builder.Property(l => l.StartTime)
            .IsRequired();

        builder.Property(l => l.EndTime)
            .IsRequired();

        builder.Property(l => l.Notes)
            .HasMaxLength(500);

        builder.Property(l => l.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(l => l.SectionId)
            .IsRequired();

        builder.HasMany(l => l.AttendanceRecords)
            .WithOne(a => a.Lecture)
            .HasForeignKey(a => a.LectureId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.SectionTasks)
            .WithOne()
            .HasForeignKey(t => t.LectureId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
