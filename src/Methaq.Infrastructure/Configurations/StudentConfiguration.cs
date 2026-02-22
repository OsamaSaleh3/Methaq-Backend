using Methaq.Domain.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.GuardianName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.GuardianPhone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.GuardianEmail)
            .HasMaxLength(100);

        builder.Property(s => s.AcademicLevel)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.SectionId);

        builder.HasOne(s => s.Section)
            .WithMany(sec => sec.Students)
            .HasForeignKey(s => s.SectionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
