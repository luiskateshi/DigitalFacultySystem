using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;


        //////////////////////////////////////////////////////////////////////////////////////ADD HERE
        public IStudentRepository Students { get; }

        public IAcademicYearRepository AcademicYears { get; }

        public IDepartmentRepository Departments { get; }

        public IDegreeProgramRepository DegreePrograms { get; }

        public ILecturerRepository Lecturers { get; }

        public IStudyPlanRepository StudyPlans { get; }

        public ISubjectRepository Subjects { get; }

        public IGenerationRepository Generations { get; }

        public IGroupRepository Groups { get; }

        public ICourseRepository Courses { get; }

        public IExamsSessionRepository ExamsSessions { get; }

        public IStudentInGroupRepository StudentsInGroups { get; }
        public IStudyPlanSubjectRepository StudyPlanSubjects { get; }
        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            //////////////////////////////////////////////////////////////////////////////////////ADD HERE
            _context = context;
            var logger = loggerFactory.CreateLogger("Logers");
            Students = new StudentRepository(_context, logger);
            AcademicYears = new AcademicYearRepository(_context, logger);
            Departments = new DepartmentRepository(_context, logger);
            DegreePrograms = new DegreeProgramRepository(_context, logger);
            Lecturers = new LecturerRepository(_context, logger);
            StudyPlans = new StudyPlanRepository(_context, logger);
            Subjects = new SubjectRepository(_context, logger);
            Generations = new GenerationRepository(_context, logger);
            Groups = new GroupRepository(_context, logger);
            Courses = new CourseRepository(_context, logger);
            ExamsSessions = new ExamsSessionRepository(_context, logger);
            StudentsInGroups = new StudentInGroupRepository(_context, logger);
            StudyPlanSubjects = new StudyPlanSubjectRepository(_context, logger);
            
        }

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
