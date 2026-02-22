using Methaq.Domain.Sections;
using Methaq.Domain.Sections.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.AcademicLevel)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(s => s.CenterId)
            .IsRequired();

        builder.Property(s => s.SupervisorId)
            .IsRequired();

        builder.OwnsOne(s => s.Schedule, schedule =>
        {
            schedule.Property(sc => sc.StartTime)
                .IsRequired()
                .HasColumnName("ScheduleStartTime");

            schedule.Property(sc => sc.EndTime)
                .IsRequired()
                .HasColumnName("ScheduleEndTime");

            schedule.Property(sc => sc.Days)
                .IsRequired()
                .HasColumnName("ScheduleDays")
                .HasConversion(
                    days => string.Join(",", days.Select(d => (int)d)),
                    str => str.Split(",", StringSplitOptions.RemoveEmptyEntries)
                              .Select(d => (DayOfWeek)int.Parse(d))
                              .ToList()
                                        )
                        .Metadata.SetValueComparer(
                            new ValueComparer<List<DayOfWeek>>(
                                (c1, c2) => c1!.SequenceEqual(c2!),
                                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                                c => c.ToList()
                            )
    );
        });

        builder.HasMany(s => s.Lectures)
            .WithOne(l => l.Section)
            .HasForeignKey(l => l.SectionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
