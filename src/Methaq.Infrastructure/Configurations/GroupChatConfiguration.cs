using Methaq.Domain.GroupChats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class GroupChatConfiguration : IEntityTypeConfiguration<GroupChat>
{
    public void Configure(EntityTypeBuilder<GroupChat> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.SectionId)
            .IsRequired();

        builder.HasOne(g => g.Section)
            .WithMany()
            .HasForeignKey(g => g.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(g => g.Members)
            .WithMany()
            .UsingEntity(j => j.ToTable("GroupChatMembers"));

        builder.Navigation(g => g.Members)
        .HasField("_members")
        .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(g => g.Messages)
            .WithOne()
            .HasForeignKey(m => m.GroupChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
