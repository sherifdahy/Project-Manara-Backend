using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_doctor_and_instructor_foreign_key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "ProgramSubjectPeriodDay",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "ProgramSubjectPeriodDay",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubjectPeriodDay_DoctorId",
                table: "ProgramSubjectPeriodDay",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubjectPeriodDay_InstructorId",
                table: "ProgramSubjectPeriodDay",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramSubjectPeriodDay_DepartmentUsers_DoctorId",
                table: "ProgramSubjectPeriodDay",
                column: "DoctorId",
                principalTable: "DepartmentUsers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramSubjectPeriodDay_DepartmentUsers_InstructorId",
                table: "ProgramSubjectPeriodDay",
                column: "InstructorId",
                principalTable: "DepartmentUsers",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramSubjectPeriodDay_DepartmentUsers_DoctorId",
                table: "ProgramSubjectPeriodDay");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramSubjectPeriodDay_DepartmentUsers_InstructorId",
                table: "ProgramSubjectPeriodDay");

            migrationBuilder.DropIndex(
                name: "IX_ProgramSubjectPeriodDay_DoctorId",
                table: "ProgramSubjectPeriodDay");

            migrationBuilder.DropIndex(
                name: "IX_ProgramSubjectPeriodDay_InstructorId",
                table: "ProgramSubjectPeriodDay");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "ProgramSubjectPeriodDay");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "ProgramSubjectPeriodDay");
        }
    }
}
