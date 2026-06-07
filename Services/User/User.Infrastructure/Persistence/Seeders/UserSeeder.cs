using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Infrastructure.Persistence.Seeders;

public class UserSeeder : IEntityTypeConfiguration<User.Domain.User>
{
    private const string AdminRoleId = "019e90e3-5c8a-7d3f-a7b6-802a4bbf19fa";
    private const string UserRoleId  = "019e90e3-5c8b-7473-a13e-84d963bf804c";

    public void Configure(EntityTypeBuilder<User.Domain.User> builder)
    {
        builder.ToTable("users");

        builder.HasData(
            new User.Domain.User
            {
                Id = "019e90e4-1a2b-7c3d-8e9f-123456789abc",
                Username = "Admin",
                Email = "admin@shooper.com",
                Password = "$2a$11$PULmazE419kkQjXFnlxpe..e65i7OXe3a9Ms8Ap/8Ek6LgrbS8BXK", // Password: admin123!
                PhoneNumber = "08123456789",
                Gender = "male",
                Image = null,
                Balance = 0,
                RoleId = AdminRoleId,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User.Domain.User
            {
                Id = "019e90e3-5c8b-7d58-b353-d0bc9b984fce",
                Username = "User",
                Email = "user@shooper.com",
                Password = "$2a$11$qulLbL/OUWKfbsO0F0f2ZeIOw9oxTUt9FK6jWnVPOYO6BGK3KKHBq", // Password: user123!
                PhoneNumber = "08987654321",
                Gender = "male",
                Image = null,
                Balance = 0,
                RoleId = UserRoleId,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}