using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_lecture_and_section_registration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramSubjectPeriodDay");

            migrationBuilder.CreateTable(
                name: "LectureSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    YearTermId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    MaxSlots = table.Column<int>(type: "int", nullable: false),
                    YearTermYearId = table.Column<int>(type: "int", nullable: false),
                    YearTermTermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureSchedule_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureSchedule_DepartmentUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DepartmentUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureSchedule_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureSchedule_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureSchedule_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureSchedule_YearTerm_YearTermYearId_YearTermTermId",
                        columns: x => new { x.YearTermYearId, x.YearTermTermId },
                        principalTable: "YearTerm",
                        principalColumns: new[] { "YearId", "TermId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    YearTermId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    MaxSlots = table.Column<int>(type: "int", nullable: false),
                    YearTermYearId = table.Column<int>(type: "int", nullable: false),
                    YearTermTermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionSchedule_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSchedule_DepartmentUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "DepartmentUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSchedule_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSchedule_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSchedule_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSchedule_YearTerm_YearTermYearId_YearTermTermId",
                        columns: x => new { x.YearTermYearId, x.YearTermTermId },
                        principalTable: "YearTerm",
                        principalColumns: new[] { "YearId", "TermId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LectureRegistration",
                columns: table => new
                {
                    LectureScheduleId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    GPA = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureRegistration", x => new { x.StudentId, x.LectureScheduleId });
                    table.ForeignKey(
                        name: "FK_LectureRegistration_LectureSchedule_LectureScheduleId",
                        column: x => x.LectureScheduleId,
                        principalTable: "LectureSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureRegistration_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionRegistration",
                columns: table => new
                {
                    SectionScheduleId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionRegistration", x => new { x.StudentId, x.SectionScheduleId });
                    table.ForeignKey(
                        name: "FK_SectionRegistration_SectionSchedule_SectionScheduleId",
                        column: x => x.SectionScheduleId,
                        principalTable: "SectionSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionRegistration_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LectureRegistration_LectureScheduleId",
                table: "LectureRegistration",
                column: "LectureScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedule_DayId",
                table: "LectureSchedule",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedule_DoctorId",
                table: "LectureSchedule",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedule_PeriodId",
                table: "LectureSchedule",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedule_ProgramId",
                table: "LectureSchedule",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedule_SubjectId",
                table: "LectureSchedule",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedule_YearTermYearId_YearTermTermId",
                table: "LectureSchedule",
                columns: new[] { "YearTermYearId", "YearTermTermId" });

            migrationBuilder.CreateIndex(
                name: "IX_SectionRegistration_SectionScheduleId",
                table: "SectionRegistration",
                column: "SectionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedule_DayId",
                table: "SectionSchedule",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedule_InstructorId",
                table: "SectionSchedule",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedule_PeriodId",
                table: "SectionSchedule",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedule_ProgramId",
                table: "SectionSchedule",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedule_SubjectId",
                table: "SectionSchedule",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedule_YearTermYearId_YearTermTermId",
                table: "SectionSchedule",
                columns: new[] { "YearTermYearId", "YearTermTermId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LectureRegistration");

            migrationBuilder.DropTable(
                name: "SectionRegistration");

            migrationBuilder.DropTable(
                name: "LectureSchedule");

            migrationBuilder.DropTable(
                name: "SectionSchedule");

            migrationBuilder.CreateTable(
                name: "ProgramSubjectPeriodDay",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    InstructorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSubjectPeriodDay", x => new { x.ProgramId, x.SubjectId, x.PeriodId, x.DayId });
                    table.ForeignKey(
                        name: "FK_ProgramSubjectPeriodDay_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSubjectPeriodDay_DepartmentUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DepartmentUsers",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ProgramSubjectPeriodDay_DepartmentUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "DepartmentUsers",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ProgramSubjectPeriodDay_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSubjectPeriodDay_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSubjectPeriodDay_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubjectPeriodDay_DayId",
                table: "ProgramSubjectPeriodDay",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubjectPeriodDay_DoctorId",
                table: "ProgramSubjectPeriodDay",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubjectPeriodDay_InstructorId",
                table: "ProgramSubjectPeriodDay",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubjectPeriodDay_PeriodId",
                table: "ProgramSubjectPeriodDay",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubjectPeriodDay_SubjectId",
                table: "ProgramSubjectPeriodDay",
                column: "SubjectId");
        }
    }
}
