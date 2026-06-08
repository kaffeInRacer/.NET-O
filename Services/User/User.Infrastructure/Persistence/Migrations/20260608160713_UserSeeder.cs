using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "balance", "created_at", "deleted_at", "email", "gender", "image", "password", "phone_number", "role_id", "username" },
                values: new object[,]
                {
                    { "019e90e3-5c8b-7d58-b353-d0bc9b984fce", 0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "user@shooper.com", "male", null, "$2a$11$qulLbL/OUWKfbsO0F0f2ZeIOw9oxTUt9FK6jWnVPOYO6BGK3KKHBq", "08987654321", "019e90e3-5c8b-7473-a13e-84d963bf804c", "User" },
                    { "019e90e4-1a2b-7c3d-8e9f-123456789abc", 0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin@shooper.com", "male", null, "$2a$11$PULmazE419kkQjXFnlxpe..e65i7OXe3a9Ms8Ap/8Ek6LgrbS8BXK", "08123456789", "019e90e3-5c8a-7d3f-a7b6-802a4bbf19fa", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "019e90e3-5c8b-7d58-b353-d0bc9b984fce");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "019e90e4-1a2b-7c3d-8e9f-123456789abc");
        }
    }
}
