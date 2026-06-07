using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Infrastructure.Persistence.Configuration;

public class RolePermissionConfiguration : IEntityTypeConfiguration<Domain.RolePermission>
{
    public void Configure(EntityTypeBuilder<Domain.RolePermission> builder)
    {
        // Junction table for Role <-> Permission Many-to-Many relationship
        builder.ToTable("role_permissions");

        // composite primary key (RoleId, PermissionId)
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.Property(rp => rp.RoleId)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("role_id");

        builder.Property(rp => rp.PermissionId)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("permission_id");

        builder.Property(rp => rp.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at")
            .HasDefaultValueSql("(getdate())");

        // Relationships are configured in RoleConfiguration and PermissionConfiguration
    }
}