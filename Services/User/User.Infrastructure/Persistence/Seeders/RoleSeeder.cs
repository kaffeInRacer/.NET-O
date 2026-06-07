using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain;

namespace User.Infrastructure.Persistence.Seeders;

public class RoleSeeder : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        
        builder.HasData(
            new Role
            {
                Id = "019e90e3-5c8a-7d3f-a7b6-802a4bbf19fa",
                Name = "Admin",
                Description = "Admin Roles Action",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Role
            {
                Id = "019e90e3-5c8b-7473-a13e-84d963bf804c",
                Name = "User",
                Description = "User Roles Action",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}