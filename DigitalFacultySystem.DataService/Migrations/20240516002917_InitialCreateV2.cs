using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFacultySystem.DataService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamRetakeRequests_Courses_CourseId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamRetakeRequests_ExamsSessions_ExamsSessionId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropIndex(
                name: "IX_ExamRetakeRequests_CourseId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropIndex(
                name: "IX_ExamRetakeRequests_ExamsSessionId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropColumn(
                name: "ExamsSessionId",
                table: "ExamRetakeRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "ExamRetakeRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExamsSessionId",
                table: "ExamRetakeRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamRetakeRequests_CourseId",
                table: "ExamRetakeRequests",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRetakeRequests_ExamsSessionId",
                table: "ExamRetakeRequests",
                column: "ExamsSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRetakeRequests_Courses_CourseId",
                table: "ExamRetakeRequests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRetakeRequests_ExamsSessions_ExamsSessionId",
                table: "ExamRetakeRequests",
                column: "ExamsSessionId",
                principalTable: "ExamsSessions",
                principalColumn: "Id");
        }
    }
}
