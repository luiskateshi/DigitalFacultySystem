using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFacultySystem.DataService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStudentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ExamsSessions");

            migrationBuilder.AddColumn<Guid>(
                name: "DegreeProgramId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isAttended",
                table: "CourseAttendances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Students_DegreeProgramId",
                table: "Students",
                column: "DegreeProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_DegreePrograms_DegreeProgramId",
                table: "Students",
                column: "DegreeProgramId",
                principalTable: "DegreePrograms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_DegreePrograms_DegreeProgramId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DegreeProgramId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DegreeProgramId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "isAttended",
                table: "CourseAttendances");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ExamsSessions",
                type: "bit",
                nullable: true);
        }
    }
}
