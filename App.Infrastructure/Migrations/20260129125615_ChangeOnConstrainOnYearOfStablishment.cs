using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOnConstrainOnYearOfStablishment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_University_YearOfEstablishment",
                table: "Universities",
                sql: "[YearOfEstablishment] >= 1800 AND [YearOfEstablishment] <= YEAR(GETDATE())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_University_YearOfEstablishment",
                table: "Universities");
        }
    }
}
