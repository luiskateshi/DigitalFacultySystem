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

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            //////////////////////////////////////////////////////////////////////////////////////ADD HERE
            _context = context;
            var logger = loggerFactory.CreateLogger("Logers");
            Students = new StudentRepository(_context, logger);
            AcademicYears = new AcademicYearRepository(_context, logger);
            Departments = new DepartmentRepository(_context, logger);
            DegreePrograms = new DegreeProgramRepository(_context, logger);
            
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
