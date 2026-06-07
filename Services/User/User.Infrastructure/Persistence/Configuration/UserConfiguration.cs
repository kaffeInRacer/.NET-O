using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<Domain.User>
{
    public void Configure(EntityTypeBuilder<Domain.User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("id");

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("username");

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("email");

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("password");

        builder.Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasColumnName("phone_number");

        builder.Property(u => u.Gender)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnName("gender");

        builder.Property(u => u.Image)
            .HasMaxLength(500)
            .IsUnicode(false)
            .HasColumnName("image");

        builder.Property(u => u.Balance)
            .HasColumnType("decimal(18,2)")
            .HasColumnName("balance");

        builder.Property(u => u.RoleId)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("role_id");

        builder.Property(u => u.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at")
            .HasDefaultValueSql("(getdate())");

        builder.Property(u => u.UpdatedAt)
            .HasColumnType("datetime")
            .HasColumnName("updated_at")
            .ValueGeneratedOnUpdate();

        builder.Property(u => u.DeletedAt)
            .IsRequired(false)
            .HasColumnName("deleted_at");

        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.PhoneNumber).IsUnique();

        // User -> Role (Many-to-One): each user has exactly one role
        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}