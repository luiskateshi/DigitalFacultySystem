using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFacultySystem.DataService.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlanSubjectId",
                table: "ExamRetakeRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudyPlanSubjectId",
                table: "ExamRetakeRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamRetakeRequests_StudyPlanSubjectId",
                table: "ExamRetakeRequests",
                column: "StudyPlanSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRetakeRequests_StudyPlanSubjects_StudyPlanSubjectId",
                table: "ExamRetakeRequests",
                column: "StudyPlanSubjectId",
                principalTable: "StudyPlanSubjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamRetakeRequests_StudyPlanSubjects_StudyPlanSubjectId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropIndex(
                name: "IX_ExamRetakeRequests_StudyPlanSubjectId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropColumn(
                name: "PlanSubjectId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropColumn(
                name: "StudyPlanSubjectId",
                table: "ExamRetakeRequests");
        }
    }
}
