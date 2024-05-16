using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFacultySystem.DataService.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdate11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "ExamRetakeRequests");

            migrationBuilder.DropColumn(
                name: "ExamsSessionId",
                table: "ExamRetakeRequests");
        }

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
        }
    }
}
