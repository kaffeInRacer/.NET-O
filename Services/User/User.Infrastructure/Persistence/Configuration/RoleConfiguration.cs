using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Infrastructure.Persistence.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Domain.Role>
{
    public void Configure(EntityTypeBuilder<Domain.Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("id");

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasColumnName("name");

        builder.Property(r => r.Description)
            .HasMaxLength(500)
            .IsUnicode(false)
            .HasColumnName("description");

        builder.Property(r => r.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at")
            .HasDefaultValueSql("(getdate())");

        builder.Property(r => r.UpdatedAt)
            .HasColumnType("datetime")
            .HasColumnName("updated_at")
            .ValueGeneratedOnUpdate();

        builder.Property(r => r.DeletedAt)
            .IsRequired(false)
            .HasColumnName("deleted_at");

        builder.HasIndex(r => r.Name).IsUnique();

        // Role -> RolePermissions (One-to-Many for Many-to-Many with Permission)
        builder.HasMany(r => r.RolePermissions)
            .WithOne(rp => rp.Role)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}