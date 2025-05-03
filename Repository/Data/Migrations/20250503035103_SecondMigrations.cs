using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Doctors_DoctorsID",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_FacultyYearSemister_FacultyYearSemisterID",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Instructors_InstructorsID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_DoctorsID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_FacultyYearSemisterID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_InstructorsID",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "DoctorsID",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "FacultyYearSemisterID",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "InstructorsID",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "Fac_ID",
                table: "FacultyYear",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fac_ID",
                table: "FacultyYear");

            migrationBuilder.AddColumn<int>(
                name: "DoctorsID",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultyYearSemisterID",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstructorsID",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_DoctorsID",
                table: "Subjects",
                column: "DoctorsID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FacultyYearSemisterID",
                table: "Subjects",
                column: "FacultyYearSemisterID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_InstructorsID",
                table: "Subjects",
                column: "InstructorsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Doctors_DoctorsID",
                table: "Subjects",
                column: "DoctorsID",
                principalTable: "Doctors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_FacultyYearSemister_FacultyYearSemisterID",
                table: "Subjects",
                column: "FacultyYearSemisterID",
                principalTable: "FacultyYearSemister",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Instructors_InstructorsID",
                table: "Subjects",
                column: "InstructorsID",
                principalTable: "Instructors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
