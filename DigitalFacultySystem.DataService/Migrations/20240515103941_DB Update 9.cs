using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFacultySystem.DataService.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdate9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Courses_LabLecturerId",
                table: "Courses",
                column: "LabLecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MainLecturerId",
                table: "Courses",
                column: "MainLecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SecondLecturerId",
                table: "Courses",
                column: "SecondLecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Lecturers_LabLecturerId",
                table: "Courses",
                column: "LabLecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Lecturers_MainLecturerId",
                table: "Courses",
                column: "MainLecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Lecturers_SecondLecturerId",
                table: "Courses",
                column: "SecondLecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Lecturers_LabLecturerId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Lecturers_MainLecturerId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Lecturers_SecondLecturerId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LabLecturerId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MainLecturerId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SecondLecturerId",
                table: "Courses");
        }
    }
}
