﻿// <auto-generated />
using System;
using DigitalFacultySystem.DataService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DigitalFacultySystem.DataService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240509112709_DB Update 4")]
    partial class DBUpdate4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.AcademicYear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("date");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_AcademicYears_Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("AcademicYears");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LabLecturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MainLecturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("SecondLecturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudyPlanSubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("StudyPlanSubjectId");

                    b.HasIndex(new[] { "Name" }, "IX_Courses_Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.CourseAttendance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LabHoursAttended")
                        .HasColumnType("int");

                    b.Property<int?>("SeminarHoursAttended")
                        .HasColumnType("int");

                    b.Property<Guid?>("StudentInCourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<bool>("isAttended")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("StudentInCourseId");

                    b.ToTable("CourseAttendances");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.DegreeProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("StudyLength")
                        .HasColumnType("int");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex(new[] { "Name" }, "IX_DegreePrograms_Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("DegreePrograms");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_Departments_Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Exam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly?>("Date")
                        .HasColumnType("date");

                    b.Property<Guid?>("ExamSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PlanSubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ExamSessionId");

                    b.HasIndex("PlanSubjectId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.ExamRetakeRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly?>("DateOfRequest")
                        .HasColumnType("date");

                    b.Property<Guid?>("ExamSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ExamSessionId");

                    b.HasIndex("StudentId");

                    b.ToTable("ExamRetakeRequests");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.ExamsSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AcademicYearId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("date");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex(new[] { "EndDate" }, "IX_ExamsSessions_EndDate");

                    b.HasIndex(new[] { "StartDate" }, "IX_ExamsSessions_StartDate");

                    b.ToTable("ExamsSessions");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Generation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CurrentSemesterOfStudies")
                        .HasColumnType("int");

                    b.Property<Guid?>("DegreeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("StartAcademicYearId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudyPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DegreeId");

                    b.HasIndex("StartAcademicYearId");

                    b.HasIndex("StudyPlanId");

                    b.HasIndex(new[] { "Name" }, "IX_Generations_Name")
                        .IsUnique();

                    b.ToTable("Generations");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("GenerationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GenerationId");

                    b.HasIndex(new[] { "Name" }, "IX_Groups_Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Lecturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("Birthdate")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "IX_Lecturers_UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("Birthdate")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DegreeProgramId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DegreeProgramId");

                    b.HasIndex(new[] { "UserId" }, "IX_Students_UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudentInCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AcademicYearId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentInCoursees");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudentsInExam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Attended")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExamGrade")
                        .HasColumnType("int");

                    b.Property<Guid?>("ExamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentsInExams");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudentsInGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudentId")
                        .IsUnique()
                        .HasFilter("[StudentId] IS NOT NULL");

                    b.ToTable("StudentsInGroups");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudyPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DegreeProgId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("date");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DegreeProgId");

                    b.HasIndex(new[] { "Name" }, "IX_StudyPlans_Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("StudyPlans");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudyPlanSubject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreditsNo")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Semester")
                        .HasColumnType("int");

                    b.Property<Guid?>("StudyPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("TotalHoursLab")
                        .HasColumnType("int");

                    b.Property<int?>("TotalHoursLectures")
                        .HasColumnType("int");

                    b.Property<int?>("TotalHoursSeminars")
                        .HasColumnType("int");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("StudyPlanId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudyPlanSubjects");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Syllabus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_Subjects_Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("CreatedOn")
                        .HasColumnType("date");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Username" }, "IX_Users_Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Course", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.StudyPlanSubject", "StudyPlanSubject")
                        .WithMany("Courses")
                        .HasForeignKey("StudyPlanSubjectId");

                    b.Navigation("StudyPlanSubject");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.CourseAttendance", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.StudentInCourse", "StudentInCourse")
                        .WithMany("CourseAttendances")
                        .HasForeignKey("StudentInCourseId");

                    b.Navigation("StudentInCourse");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.DegreeProgram", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.Department", "Department")
                        .WithMany("DegreePrograms")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Exam", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.ExamsSession", "ExamSession")
                        .WithMany("Exams")
                        .HasForeignKey("ExamSessionId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.StudyPlanSubject", "PlanSubject")
                        .WithMany("Exams")
                        .HasForeignKey("PlanSubjectId");

                    b.Navigation("ExamSession");

                    b.Navigation("PlanSubject");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.ExamRetakeRequest", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.Course", "Course")
                        .WithMany("ExamRetakeRequests")
                        .HasForeignKey("CourseId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.ExamsSession", "ExamSession")
                        .WithMany("ExamRetakeRequests")
                        .HasForeignKey("ExamSessionId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.Student", "Student")
                        .WithMany("ExamRetakeRequests")
                        .HasForeignKey("StudentId");

                    b.Navigation("Course");

                    b.Navigation("ExamSession");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.ExamsSession", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.AcademicYear", "AcademicYear")
                        .WithMany("ExamsSessions")
                        .HasForeignKey("AcademicYearId");

                    b.Navigation("AcademicYear");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Generation", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.DegreeProgram", "Degree")
                        .WithMany("Generations")
                        .HasForeignKey("DegreeId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.AcademicYear", "StartAcademicYear")
                        .WithMany("Generations")
                        .HasForeignKey("StartAcademicYearId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.StudyPlan", "StudyPlan")
                        .WithMany("Generations")
                        .HasForeignKey("StudyPlanId");

                    b.Navigation("Degree");

                    b.Navigation("StartAcademicYear");

                    b.Navigation("StudyPlan");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Group", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.Generation", "Generation")
                        .WithMany("Groups")
                        .HasForeignKey("GenerationId");

                    b.Navigation("Generation");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Lecturer", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.User", "User")
                        .WithMany("Lecturers")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Student", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.DegreeProgram", "DegreeProgram")
                        .WithMany()
                        .HasForeignKey("DegreeProgramId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.User", "User")
                        .WithMany("Students")
                        .HasForeignKey("UserId");

                    b.Navigation("DegreeProgram");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudentInCourse", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.AcademicYear", "AcademicYear")
                        .WithMany("StudentInCourses")
                        .HasForeignKey("AcademicYearId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.Course", "Course")
                        .WithMany("StudentInCourses")
                        .HasForeignKey("CourseId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.Student", "Student")
                        .WithMany("StudentInCourses")
                        .HasForeignKey("StudentId");

                    b.Navigation("AcademicYear");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudentsInExam", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.Exam", "Exam")
                        .WithMany("StudentsInExams")
                        .HasForeignKey("ExamId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.Student", "Student")
                        .WithMany("StudentsInExams")
                        .HasForeignKey("StudentId");

                    b.Navigation("Exam");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudentsInGroup", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.Group", "Group")
                        .WithMany("StudentsInGroups")
                        .HasForeignKey("GroupId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.Student", "Student")
                        .WithOne("StudentsInGroup")
                        .HasForeignKey("DigitalFacultySystem.Domain.Entities.StudentsInGroup", "StudentId");

                    b.Navigation("Group");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudyPlan", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.DegreeProgram", "DegreeProg")
                        .WithMany("StudyPlans")
                        .HasForeignKey("DegreeProgId");

                    b.Navigation("DegreeProg");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudyPlanSubject", b =>
                {
                    b.HasOne("DigitalFacultySystem.Domain.Entities.StudyPlan", "StudyPlan")
                        .WithMany("StudyPlanSubjects")
                        .HasForeignKey("StudyPlanId");

                    b.HasOne("DigitalFacultySystem.Domain.Entities.Subject", "Subject")
                        .WithMany("StudyPlanSubjects")
                        .HasForeignKey("SubjectId");

                    b.Navigation("StudyPlan");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.AcademicYear", b =>
                {
                    b.Navigation("ExamsSessions");

                    b.Navigation("Generations");

                    b.Navigation("StudentInCourses");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Course", b =>
                {
                    b.Navigation("ExamRetakeRequests");

                    b.Navigation("StudentInCourses");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.DegreeProgram", b =>
                {
                    b.Navigation("Generations");

                    b.Navigation("StudyPlans");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Department", b =>
                {
                    b.Navigation("DegreePrograms");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Exam", b =>
                {
                    b.Navigation("StudentsInExams");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.ExamsSession", b =>
                {
                    b.Navigation("ExamRetakeRequests");

                    b.Navigation("Exams");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Generation", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Group", b =>
                {
                    b.Navigation("StudentsInGroups");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Student", b =>
                {
                    b.Navigation("ExamRetakeRequests");

                    b.Navigation("StudentInCourses");

                    b.Navigation("StudentsInExams");

                    b.Navigation("StudentsInGroup");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudentInCourse", b =>
                {
                    b.Navigation("CourseAttendances");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudyPlan", b =>
                {
                    b.Navigation("Generations");

                    b.Navigation("StudyPlanSubjects");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.StudyPlanSubject", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Exams");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.Subject", b =>
                {
                    b.Navigation("StudyPlanSubjects");
                });

            modelBuilder.Entity("DigitalFacultySystem.Domain.Entities.User", b =>
                {
                    b.Navigation("Lecturers");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
