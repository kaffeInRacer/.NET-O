using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain;

namespace User.Infrastructure.Persistence.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Id)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("id");

        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(256)
            .IsUnicode(false)
            .HasColumnName("token");

        builder.Property(rt => rt.UserId)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("user_id");

        builder.Property(rt => rt.ExpiresAt)
            .HasColumnType("datetime")
            .HasColumnName("expires_at");

        builder.Property(rt => rt.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at")
            .HasDefaultValueSql("(getdate())");

        builder.Property(rt => rt.IsRevoked)
            .HasColumnName("is_revoked")
            .HasDefaultValue(false);

        builder.Property(rt => rt.DeviceName)
            .IsRequired()
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("device_name");

        builder.HasIndex(rt => rt.Token).IsUnique();

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}