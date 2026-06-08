using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "description", "name" },
                values: new object[,]
                {
                    { "019e90e3-5c8a-7d3f-a7b6-802a4bbf19fa", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Admin Roles Action", "Admin" },
                    { "019e90e3-5c8b-7473-a13e-84d963bf804c", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "User Roles Action", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "019e90e3-5c8a-7d3f-a7b6-802a4bbf19fa");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "019e90e3-5c8b-7473-a13e-84d963bf804c");
        }
    }
}
