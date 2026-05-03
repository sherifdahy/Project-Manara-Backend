using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_lecture_and_section_schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureRegistration_LectureSchedule_LectureScheduleId",
                table: "LectureRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureRegistration_Students_StudentId",
                table: "LectureRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedule_Day_DayId",
                table: "LectureSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedule_DepartmentUsers_DoctorId",
                table: "LectureSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedule_Period_PeriodId",
                table: "LectureSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedule_Programs_ProgramId",
                table: "LectureSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedule_Subjects_SubjectId",
                table: "LectureSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedule_YearTerm_YearTermYearId_YearTermTermId",
                table: "LectureSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionRegistration_SectionSchedule_SectionScheduleId",
                table: "SectionRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionRegistration_Students_StudentId",
                table: "SectionRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedule_Day_DayId",
                table: "SectionSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedule_DepartmentUsers_InstructorId",
                table: "SectionSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedule_Period_PeriodId",
                table: "SectionSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedule_Programs_ProgramId",
                table: "SectionSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedule_Subjects_SubjectId",
                table: "SectionSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedule_YearTerm_YearTermYearId_YearTermTermId",
                table: "SectionSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionSchedule",
                table: "SectionSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionRegistration",
                table: "SectionRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LectureSchedule",
                table: "LectureSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LectureRegistration",
                table: "LectureRegistration");

            migrationBuilder.RenameTable(
                name: "SectionSchedule",
                newName: "SectionSchedules");

            migrationBuilder.RenameTable(
                name: "SectionRegistration",
                newName: "SectionRegistrations");

            migrationBuilder.RenameTable(
                name: "LectureSchedule",
                newName: "LectureSchedules");

            migrationBuilder.RenameTable(
                name: "LectureRegistration",
                newName: "LectureRegistrations");

            migrationBuilder.RenameColumn(
                name: "YearTermId",
                table: "SectionSchedules",
                newName: "YearId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedule_YearTermYearId_YearTermTermId",
                table: "SectionSchedules",
                newName: "IX_SectionSchedules_YearTermYearId_YearTermTermId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedule_SubjectId",
                table: "SectionSchedules",
                newName: "IX_SectionSchedules_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedule_ProgramId",
                table: "SectionSchedules",
                newName: "IX_SectionSchedules_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedule_PeriodId",
                table: "SectionSchedules",
                newName: "IX_SectionSchedules_PeriodId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedule_InstructorId",
                table: "SectionSchedules",
                newName: "IX_SectionSchedules_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedule_DayId",
                table: "SectionSchedules",
                newName: "IX_SectionSchedules_DayId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionRegistration_SectionScheduleId",
                table: "SectionRegistrations",
                newName: "IX_SectionRegistrations_SectionScheduleId");

            migrationBuilder.RenameColumn(
                name: "YearTermId",
                table: "LectureSchedules",
                newName: "YearId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedule_YearTermYearId_YearTermTermId",
                table: "LectureSchedules",
                newName: "IX_LectureSchedules_YearTermYearId_YearTermTermId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedule_SubjectId",
                table: "LectureSchedules",
                newName: "IX_LectureSchedules_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedule_ProgramId",
                table: "LectureSchedules",
                newName: "IX_LectureSchedules_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedule_PeriodId",
                table: "LectureSchedules",
                newName: "IX_LectureSchedules_PeriodId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedule_DoctorId",
                table: "LectureSchedules",
                newName: "IX_LectureSchedules_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedule_DayId",
                table: "LectureSchedules",
                newName: "IX_LectureSchedules_DayId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureRegistration_LectureScheduleId",
                table: "LectureRegistrations",
                newName: "IX_LectureRegistrations_LectureScheduleId");

            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "SectionSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "LectureSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionSchedules",
                table: "SectionSchedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionRegistrations",
                table: "SectionRegistrations",
                columns: new[] { "StudentId", "SectionScheduleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LectureSchedules",
                table: "LectureSchedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LectureRegistrations",
                table: "LectureRegistrations",
                columns: new[] { "StudentId", "LectureScheduleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LectureRegistrations_LectureSchedules_LectureScheduleId",
                table: "LectureRegistrations",
                column: "LectureScheduleId",
                principalTable: "LectureSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureRegistrations_Students_StudentId",
                table: "LectureRegistrations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_Day_DayId",
                table: "LectureSchedules",
                column: "DayId",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_DepartmentUsers_DoctorId",
                table: "LectureSchedules",
                column: "DoctorId",
                principalTable: "DepartmentUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_Period_PeriodId",
                table: "LectureSchedules",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_Programs_ProgramId",
                table: "LectureSchedules",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_Subjects_SubjectId",
                table: "LectureSchedules",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "LectureSchedules",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionRegistrations_SectionSchedules_SectionScheduleId",
                table: "SectionRegistrations",
                column: "SectionScheduleId",
                principalTable: "SectionSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionRegistrations_Students_StudentId",
                table: "SectionRegistrations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedules_Day_DayId",
                table: "SectionSchedules",
                column: "DayId",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedules_DepartmentUsers_InstructorId",
                table: "SectionSchedules",
                column: "InstructorId",
                principalTable: "DepartmentUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedules_Period_PeriodId",
                table: "SectionSchedules",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedules_Programs_ProgramId",
                table: "SectionSchedules",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedules_Subjects_SubjectId",
                table: "SectionSchedules",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "SectionSchedules",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureRegistrations_LectureSchedules_LectureScheduleId",
                table: "LectureRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureRegistrations_Students_StudentId",
                table: "LectureRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_Day_DayId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_DepartmentUsers_DoctorId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_Period_PeriodId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_Programs_ProgramId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_Subjects_SubjectId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionRegistrations_SectionSchedules_SectionScheduleId",
                table: "SectionRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionRegistrations_Students_StudentId",
                table: "SectionRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedules_Day_DayId",
                table: "SectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedules_DepartmentUsers_InstructorId",
                table: "SectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedules_Period_PeriodId",
                table: "SectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedules_Programs_ProgramId",
                table: "SectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedules_Subjects_SubjectId",
                table: "SectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "SectionSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionSchedules",
                table: "SectionSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionRegistrations",
                table: "SectionRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LectureSchedules",
                table: "LectureSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LectureRegistrations",
                table: "LectureRegistrations");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "SectionSchedules");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "LectureSchedules");

            migrationBuilder.RenameTable(
                name: "SectionSchedules",
                newName: "SectionSchedule");

            migrationBuilder.RenameTable(
                name: "SectionRegistrations",
                newName: "SectionRegistration");

            migrationBuilder.RenameTable(
                name: "LectureSchedules",
                newName: "LectureSchedule");

            migrationBuilder.RenameTable(
                name: "LectureRegistrations",
                newName: "LectureRegistration");

            migrationBuilder.RenameColumn(
                name: "YearId",
                table: "SectionSchedule",
                newName: "YearTermId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedules_YearTermYearId_YearTermTermId",
                table: "SectionSchedule",
                newName: "IX_SectionSchedule_YearTermYearId_YearTermTermId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedules_SubjectId",
                table: "SectionSchedule",
                newName: "IX_SectionSchedule_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedules_ProgramId",
                table: "SectionSchedule",
                newName: "IX_SectionSchedule_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedules_PeriodId",
                table: "SectionSchedule",
                newName: "IX_SectionSchedule_PeriodId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedules_InstructorId",
                table: "SectionSchedule",
                newName: "IX_SectionSchedule_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSchedules_DayId",
                table: "SectionSchedule",
                newName: "IX_SectionSchedule_DayId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionRegistrations_SectionScheduleId",
                table: "SectionRegistration",
                newName: "IX_SectionRegistration_SectionScheduleId");

            migrationBuilder.RenameColumn(
                name: "YearId",
                table: "LectureSchedule",
                newName: "YearTermId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedules_YearTermYearId_YearTermTermId",
                table: "LectureSchedule",
                newName: "IX_LectureSchedule_YearTermYearId_YearTermTermId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedules_SubjectId",
                table: "LectureSchedule",
                newName: "IX_LectureSchedule_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedules_ProgramId",
                table: "LectureSchedule",
                newName: "IX_LectureSchedule_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedules_PeriodId",
                table: "LectureSchedule",
                newName: "IX_LectureSchedule_PeriodId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedules_DoctorId",
                table: "LectureSchedule",
                newName: "IX_LectureSchedule_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureSchedules_DayId",
                table: "LectureSchedule",
                newName: "IX_LectureSchedule_DayId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureRegistrations_LectureScheduleId",
                table: "LectureRegistration",
                newName: "IX_LectureRegistration_LectureScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionSchedule",
                table: "SectionSchedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionRegistration",
                table: "SectionRegistration",
                columns: new[] { "StudentId", "SectionScheduleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LectureSchedule",
                table: "LectureSchedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LectureRegistration",
                table: "LectureRegistration",
                columns: new[] { "StudentId", "LectureScheduleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LectureRegistration_LectureSchedule_LectureScheduleId",
                table: "LectureRegistration",
                column: "LectureScheduleId",
                principalTable: "LectureSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureRegistration_Students_StudentId",
                table: "LectureRegistration",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedule_Day_DayId",
                table: "LectureSchedule",
                column: "DayId",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedule_DepartmentUsers_DoctorId",
                table: "LectureSchedule",
                column: "DoctorId",
                principalTable: "DepartmentUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedule_Period_PeriodId",
                table: "LectureSchedule",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedule_Programs_ProgramId",
                table: "LectureSchedule",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedule_Subjects_SubjectId",
                table: "LectureSchedule",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedule_YearTerm_YearTermYearId_YearTermTermId",
                table: "LectureSchedule",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionRegistration_SectionSchedule_SectionScheduleId",
                table: "SectionRegistration",
                column: "SectionScheduleId",
                principalTable: "SectionSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionRegistration_Students_StudentId",
                table: "SectionRegistration",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedule_Day_DayId",
                table: "SectionSchedule",
                column: "DayId",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedule_DepartmentUsers_InstructorId",
                table: "SectionSchedule",
                column: "InstructorId",
                principalTable: "DepartmentUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedule_Period_PeriodId",
                table: "SectionSchedule",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedule_Programs_ProgramId",
                table: "SectionSchedule",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedule_Subjects_SubjectId",
                table: "SectionSchedule",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedule_YearTerm_YearTermYearId_YearTermTermId",
                table: "SectionSchedule",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
