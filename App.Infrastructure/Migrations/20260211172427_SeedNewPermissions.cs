using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedNewPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21,
                column: "ClaimValue",
                value: "Programs:read");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22,
                column: "ClaimValue",
                value: "Programs:create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23,
                column: "ClaimValue",
                value: "Programs:update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24,
                column: "ClaimValue",
                value: "Programs:toggleStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25,
                column: "ClaimValue",
                value: "facultyUsers:read");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26,
                column: "ClaimValue",
                value: "facultyUsers:create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                column: "ClaimValue",
                value: "facultyUsers:update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                column: "ClaimValue",
                value: "facultyUsers:toggleStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                column: "ClaimValue",
                value: "scopes:read");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 30, "permissions", "scopes:readDetail", 100 },
                    { 31, "permissions", "scopes:create", 100 },
                    { 32, "permissions", "scopes:update", 100 },
                    { 33, "permissions", "scopes:toggleStatus", 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21,
                column: "ClaimValue",
                value: "facultyUsers:read");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22,
                column: "ClaimValue",
                value: "facultyUsers:create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23,
                column: "ClaimValue",
                value: "facultyUsers:update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24,
                column: "ClaimValue",
                value: "facultyUsers:toggleStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25,
                column: "ClaimValue",
                value: "scopes:read");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26,
                column: "ClaimValue",
                value: "scopes:readDetail");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                column: "ClaimValue",
                value: "scopes:create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                column: "ClaimValue",
                value: "scopes:update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                column: "ClaimValue",
                value: "scopes:toggleStatus");
        }
    }
}
