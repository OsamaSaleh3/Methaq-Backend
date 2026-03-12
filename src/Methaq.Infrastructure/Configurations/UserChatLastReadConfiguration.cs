using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Methaq.Domain.GroupChats;

namespace Methaq.Infrastructure.Persistence.Configurations;

public class UserChatLastReadConfiguration : IEntityTypeConfiguration<UserChatLastRead>
{
    public void Configure(EntityTypeBuilder<UserChatLastRead> builder)
    {
        builder.HasKey(x => new { x.UserId, x.GroupChatId });

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.GroupChat)
            .WithMany()
            .HasForeignKey(x => x.GroupChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<GroupMessage>()
            .WithMany()
            .HasForeignKey(x => x.LastReadMessageId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}