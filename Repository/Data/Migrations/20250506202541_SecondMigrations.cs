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

            migrationBuilder.DropColumn(
                name: "Sub_Name",
                table: "Attendance");

            migrationBuilder.RenameColumn(
                name: "FacultyYearId",
                table: "FacultyYearSemister",
                newName: "FacYear_Id");

            migrationBuilder.AddColumn<int>(
                name: "St_ID",
                table: "Lecture",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultyID",
                table: "FacultyYear",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Dr_ID",
                table: "Subjects",
                column: "Dr_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FacYearSem_ID",
                table: "Subjects",
                column: "FacYearSem_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Ins_ID",
                table: "Subjects",
                column: "Ins_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Studets_Rooms_Room_ID",
                table: "Studets_Rooms",
                column: "Room_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Studets_Rooms_St_ID",
                table: "Studets_Rooms",
                column: "St_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Fac_ID",
                table: "Students",
                column: "Fac_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacYearSem_ID",
                table: "Students",
                column: "FacYearSem_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyYear_FacultyID",
                table: "FacultyYear",
                column: "FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyYear_Faculty_FacultyID",
                table: "FacultyYear",
                column: "FacultyID",
                principalTable: "Faculty",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_FacultyYearSemister_FacYearSem_ID",
                table: "Students",
                column: "FacYearSem_ID",
                principalTable: "FacultyYearSemister",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculty_Fac_ID",
                table: "Students",
                column: "Fac_ID",
                principalTable: "Faculty",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Studets_Rooms_Rooms_Room_ID",
                table: "Studets_Rooms",
                column: "Room_ID",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Studets_Rooms_Students_St_ID",
                table: "Studets_Rooms",
                column: "St_ID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Doctors_Dr_ID",
                table: "Subjects",
                column: "Dr_ID",
                principalTable: "Doctors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_FacultyYearSemister_FacYearSem_ID",
                table: "Subjects",
                column: "FacYearSem_ID",
                principalTable: "FacultyYearSemister",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Instructors_Ins_ID",
                table: "Subjects",
                column: "Ins_ID",
                principalTable: "Instructors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyYear_Faculty_FacultyID",
                table: "FacultyYear");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_FacultyYearSemister_FacYearSem_ID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculty_Fac_ID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Studets_Rooms_Rooms_Room_ID",
                table: "Studets_Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Studets_Rooms_Students_St_ID",
                table: "Studets_Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Doctors_Dr_ID",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_FacultyYearSemister_FacYearSem_ID",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Instructors_Ins_ID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_Dr_ID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_FacYearSem_ID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_Ins_ID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Studets_Rooms_Room_ID",
                table: "Studets_Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Studets_Rooms_St_ID",
                table: "Studets_Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Students_Fac_ID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FacYearSem_ID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_FacultyYear_FacultyID",
                table: "FacultyYear");

            migrationBuilder.DropColumn(
                name: "St_ID",
                table: "Lecture");

            migrationBuilder.DropColumn(
                name: "FacultyID",
                table: "FacultyYear");

            migrationBuilder.RenameColumn(
                name: "FacYear_Id",
                table: "FacultyYearSemister",
                newName: "FacultyYearId");

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

            migrationBuilder.AddColumn<string>(
                name: "Sub_Name",
                table: "Attendance",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

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
