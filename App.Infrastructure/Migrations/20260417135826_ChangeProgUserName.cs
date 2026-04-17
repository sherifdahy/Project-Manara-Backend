using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProgUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUsers_AspNetUsers_UserId",
                table: "ProgramUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUsers_Faculties_FacultyId",
                table: "ProgramUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentProgramYearTerm_ProgramUsers_UserId",
                table: "StudentProgramYearTerm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramUsers",
                table: "ProgramUsers");

            migrationBuilder.RenameTable(
                name: "ProgramUsers",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramUsers_FacultyId",
                table: "Students",
                newName: "IX_Students_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProgramYearTerm_Students_UserId",
                table: "StudentProgramYearTerm",
                column: "UserId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculties_FacultyId",
                table: "Students",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProgramYearTerm_Students_UserId",
                table: "StudentProgramYearTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculties_FacultyId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "ProgramUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Students_FacultyId",
                table: "ProgramUsers",
                newName: "IX_ProgramUsers_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramUsers",
                table: "ProgramUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUsers_AspNetUsers_UserId",
                table: "ProgramUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUsers_Faculties_FacultyId",
                table: "ProgramUsers",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProgramYearTerm_ProgramUsers_UserId",
                table: "StudentProgramYearTerm",
                column: "UserId",
                principalTable: "ProgramUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
