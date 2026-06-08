using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain;

namespace User.Infrastructure.Persistence.Configuration;

public class AddressConfiguration : IEntityTypeConfiguration<Addresses>
{
    public void Configure(EntityTypeBuilder<Addresses> builder)
    {
        builder.ToTable("addresses");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("id");

        builder.Property(a => a.UserId)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("user_id");

        builder.Property(a => a.VillageId)
            .IsRequired()
            .HasMaxLength(36)
            .HasColumnName("village_id");

        builder.Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("street");

        builder.Property(a => a.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at")
            .HasDefaultValueSql("(getdate())");

        builder.Property(a => a.UpdatedAt)
            .HasColumnType("datetime")
            .HasColumnName("updated_at")
            .ValueGeneratedOnUpdate();

        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
