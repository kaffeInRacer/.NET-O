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

        builder.Property(a => a.Province)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("province");

        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("city");

        builder.Property(a => a.District)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("district");

        builder.Property(a => a.ZipCode)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("zipcode");

        builder.Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("street");

        // Address -> User (Many-to-One)
        // Cascade delete: when user is deleted, all addresses are deleted
        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}