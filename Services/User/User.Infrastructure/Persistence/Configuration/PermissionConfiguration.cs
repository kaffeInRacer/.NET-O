using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Infrastructure.Persistence.Configuration;

public class PermissionConfiguration : IEntityTypeConfiguration<Domain.Permission>
{
    public void Configure(EntityTypeBuilder<Domain.Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("id");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasColumnName("name");

        builder.Property(p => p.Resource)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasColumnName("resource");

        builder.Property(p => p.Action)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("action");

        builder.Property(p => p.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at")
            .HasDefaultValueSql("(getdate())");

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("datetime")
            .HasColumnName("updated_at")
            .ValueGeneratedOnUpdate();

        builder.Property(p => p.DeletedAt)
            .IsRequired(false)
            .HasColumnName("deleted_at");

        // composite unique index for resource + action combination
        builder.HasIndex(p => new { p.Resource, p.Action }).IsUnique();

        // Permission -> RolePermissions (One-to-Many for Many-to-Many with Role)
        // Cascade delete: when permission is deleted, all role assignments are deleted
        builder.HasMany(p => p.RolePermissions)
            .WithOne(rp => rp.Permission)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}