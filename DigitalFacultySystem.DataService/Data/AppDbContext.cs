using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.DbSet;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalFacultySystem.DataService.Data
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {

        public virtual DbSet<AcademicYear> AcademicYears { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<CourseAttendance> CourseAttendances { get; set; }

        public virtual DbSet<DegreeProgram> DegreePrograms { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Exam> Exams { get; set; }

        public virtual DbSet<ExamRetakeRequest> ExamRetakeRequests { get; set; }

        public virtual DbSet<ExamsSession> ExamsSessions { get; set; }

        public virtual DbSet<Generation> Generations { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<Lecturer> Lecturers { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<StudentInCourse> StudentInCoursees { get; set; }

        public virtual DbSet<StudentsInExam> StudentsInExams { get; set; }

        public virtual DbSet<StudentsInGroup> StudentsInGroups { get; set; }

        public virtual DbSet<StudyPlan> StudyPlans { get; set; }

        public virtual DbSet<StudyPlanSubject> StudyPlanSubjects { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AcademicYear>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_AcademicYears_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Courses_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<DegreeProgram>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_DegreePrograms_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Departments_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<ExamsSession>(entity =>
            {
                entity.HasIndex(e => e.StartDate, "IX_ExamsSessions_StartDate");

                entity.HasIndex(e => e.EndDate, "IX_ExamsSessions_EndDate");
            });

            modelBuilder.Entity<Generation>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Generations_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Groups_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<Lecturer>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Lecturers_UserId")
                    .IsUnique();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Students_UserId")
                    .IsUnique();
            });

            modelBuilder.Entity<StudyPlan>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_StudyPlans_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Subjects_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "IX_Users_Username")
                    .IsUnique();
            });


        }   



    }
}
