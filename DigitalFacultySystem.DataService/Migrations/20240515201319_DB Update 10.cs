using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFacultySystem.DataService.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdate10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamRetakeRequests_ExamsSessions_ExamSessionId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamRetakeRequests_StudyPlanSubjects_StudyPlanSubjectId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropIndex(
                name: "IX_ExamRetakeRequests_ExamSessionId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropColumn(
                name: "ExamSessionId",
                table: "ExamRetakeRequests");

            migrationBuilder.RenameColumn(
                name: "StudyPlanSubjectId",
                table: "ExamRetakeRequests",
                newName: "ExamsSessionId");

            migrationBuilder.RenameColumn(
                name: "PlanSubjectId",
                table: "ExamRetakeRequests",
                newName: "ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamRetakeRequests_StudyPlanSubjectId",
                table: "ExamRetakeRequests",
                newName: "IX_ExamRetakeRequests_ExamsSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRetakeRequests_ExamId",
                table: "ExamRetakeRequests",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRetakeRequests_ExamsSessions_ExamsSessionId",
                table: "ExamRetakeRequests",
                column: "ExamsSessionId",
                principalTable: "ExamsSessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRetakeRequests_Exams_ExamId",
                table: "ExamRetakeRequests",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamRetakeRequests_ExamsSessions_ExamsSessionId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamRetakeRequests_Exams_ExamId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropIndex(
                name: "IX_ExamRetakeRequests_ExamId",
                table: "ExamRetakeRequests");

            migrationBuilder.RenameColumn(
                name: "ExamsSessionId",
                table: "ExamRetakeRequests",
                newName: "StudyPlanSubjectId");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "ExamRetakeRequests",
                newName: "PlanSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamRetakeRequests_ExamsSessionId",
                table: "ExamRetakeRequests",
                newName: "IX_ExamRetakeRequests_StudyPlanSubjectId");

            migrationBuilder.AddColumn<Guid>(
                name: "ExamSessionId",
                table: "ExamRetakeRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamRetakeRequests_ExamSessionId",
                table: "ExamRetakeRequests",
                column: "ExamSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRetakeRequests_ExamsSessions_ExamSessionId",
                table: "ExamRetakeRequests",
                column: "ExamSessionId",
                principalTable: "ExamsSessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRetakeRequests_StudyPlanSubjects_StudyPlanSubjectId",
                table: "ExamRetakeRequests",
                column: "StudyPlanSubjectId",
                principalTable: "StudyPlanSubjects",
                principalColumn: "Id");
        }
    }
}
