using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFacultySystem.DataService.Migrations
{
    /// <inheritdoc />
    public partial class BaseEntityExtraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "StudyPlanSubjects");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "StudentsInGroups");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "StudentsInExams");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "StudentInCoursees");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Generations");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ExamRetakeRequests");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "DegreePrograms");

            migrationBuilder.DropColumn(
                name: "Staus",
                table: "DegreePrograms");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "CourseAttendances");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AcademicYears");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Subjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Subjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StudyPlanSubjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "StudyPlanSubjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StudyPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "StudyPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentsInGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "StudentsInGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentsInExams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "StudentsInExams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentInCoursees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "StudentInCoursees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Lecturers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Groups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Generations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Generations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamsSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ExamsSessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "ExamsSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Exams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Exams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamRetakeRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "ExamRetakeRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DegreePrograms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "DegreePrograms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseAttendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "CourseAttendances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AcademicYears",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "AcademicYears",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudyPlanSubjects");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "StudyPlanSubjects");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudentsInGroups");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "StudentsInGroups");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudentsInExams");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "StudentsInExams");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudentInCoursees");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "StudentInCoursees");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Generations");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Generations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ExamsSessions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ExamsSessions");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "ExamsSessions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ExamRetakeRequests");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "ExamRetakeRequests");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DegreePrograms");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "DegreePrograms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CourseAttendances");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "CourseAttendances");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AcademicYears");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "AcademicYears");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Subjects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "StudyPlanSubjects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "StudyPlans",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "StudentsInGroups",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "StudentsInExams",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Students",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "StudentInCoursees",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Lecturers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Groups",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Generations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Exams",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ExamRetakeRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Departments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedOn",
                table: "Departments",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "DegreePrograms",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Staus",
                table: "DegreePrograms",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Courses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "CourseAttendances",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AcademicYears",
                type: "bit",
                nullable: true);
        }
    }
}
