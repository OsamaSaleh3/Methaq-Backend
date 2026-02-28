using Methaq.Domain.SectionTasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class StudentTaskEvaluationConfiguration : IEntityTypeConfiguration<StudentTaskEvaluation>
{
    public void Configure(EntityTypeBuilder<StudentTaskEvaluation> builder)
    {
        builder.HasKey(e => new { e.StudentId, e.SectionTaskId });

        builder.Property(e => e.AchievedMark)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(e => e.Notes)
            .HasMaxLength(300);

        builder.Property(e => e.EvaluatedAt)
            .IsRequired();

        builder.HasOne(e => e.SectionTask)
              .WithMany(t => t.Evaluations)
              .HasForeignKey(e => e.SectionTaskId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Student)
            .WithMany()
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
