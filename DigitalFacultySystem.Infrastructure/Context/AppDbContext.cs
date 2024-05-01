using DigitalFacultySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Infrastructure.Context
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DegreeProgram> DegreePrograms { get; set; }
        public DbSet<StudyPlan> StudyPlans { get; set; }
        public DbSet<Generation> Generations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudyPlanSubject> StudyPlanSubjects { get; set; }


    }
}
