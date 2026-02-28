using Methaq.Domain.SectionTasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class SectionTaskConfiguration : IEntityTypeConfiguration<SectionTask>
{
    public void Configure(EntityTypeBuilder<SectionTask> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.SectionId)
            .IsRequired();

        builder.Property(t => t.LectureId)
            .IsRequired();

        builder.Property(t => t.AssignedById)
            .IsRequired();

        builder.Property(t => t.FullMark)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(t => t.Types)
            .IsRequired()
            .HasConversion<string>();

        builder.OwnsOne(t => t.Range, range =>
        {
            range.Property(r => r.Volume)
                .HasMaxLength(50)
                .HasColumnName("RangeVolume");

            range.Property(r => r.SurahName)
                .HasMaxLength(100)
                .HasColumnName("RangeSurahName");

            range.Property(r => r.StartPage)
                .HasColumnName("RangeStartPage");

            range.Property(r => r.EndPage)
                .HasColumnName("RangeEndPage");

            range.Property(r => r.StartAyah)
                .HasColumnName("RangeStartAyah");

            range.Property(r => r.EndAyah)
                .HasColumnName("RangeEndAyah");
        });

        builder.HasOne(t => t.Student)
            .WithMany()
            .HasForeignKey(t => t.StudentId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasOne(t => t.AssignedBy)
            .WithMany()
            .HasForeignKey(t => t.AssignedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Section)
            .WithMany()
            .HasForeignKey(t => t.SectionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Evaluations)
            .WithOne(e => e.SectionTask)
            .HasForeignKey(e => e.SectionTaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}