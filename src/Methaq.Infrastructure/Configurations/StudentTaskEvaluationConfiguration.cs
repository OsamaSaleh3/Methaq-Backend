using Methaq.Domain.MemorizationTasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class StudentTaskEvaluationConfiguration : IEntityTypeConfiguration<StudentTaskEvaluation>
{
    public void Configure(EntityTypeBuilder<StudentTaskEvaluation> builder)
    {
        builder.HasKey(e => new { e.StudentId, e.UnifiedTaskId });

        builder.Property(e => e.AchievedMark)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(e => e.Notes)
            .HasMaxLength(300);

        builder.Property(e => e.EvaluatedAt)
            .IsRequired();

        builder.HasOne<Domain.Students.Student>()
            .WithMany()
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
