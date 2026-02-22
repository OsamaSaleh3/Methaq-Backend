using Methaq.Domain.GroupChats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class GroupMessageConfiguration : IEntityTypeConfiguration<GroupMessage>
{
    public void Configure(EntityTypeBuilder<GroupMessage> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.GroupChatId)
            .IsRequired();

        builder.Property(m => m.SenderId)
            .IsRequired();

        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(m => m.AttachmentUrl)
            .HasMaxLength(500);

        builder.Property(m => m.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(m => m.EditedAt);

        builder.HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
