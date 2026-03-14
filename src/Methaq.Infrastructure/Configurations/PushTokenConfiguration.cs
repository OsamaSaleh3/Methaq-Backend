using Methaq.Domain.PushTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Methaq.Infrastructure.Configurations;

public class PushTokenConfiguration : IEntityTypeConfiguration<PushToken>
{
    public void Configure(EntityTypeBuilder<PushToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Platform)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasIndex(x => x.UserId).IsUnique();
    }
}