using Methaq.Domain.Notifications;
using Methaq.Domain.Notifications.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.UserId)
            .IsRequired();

        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(n => n.Content)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(n => n.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(n => n.IsRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(n => n.RelatedEntityId);

        builder.HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
