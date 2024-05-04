using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFacultySystem.DataService.Migrations
{
    /// <inheritdoc />
    public partial class Fixesofentityprops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentsInGroups_StudentId",
                table: "StudentsInGroups");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsInGroups_StudentId",
                table: "StudentsInGroups",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentsInGroups_StudentId",
                table: "StudentsInGroups");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsInGroups_StudentId",
                table: "StudentsInGroups",
                column: "StudentId");
        }
    }
}
