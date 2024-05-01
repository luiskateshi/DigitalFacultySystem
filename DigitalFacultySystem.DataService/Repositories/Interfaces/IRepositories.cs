using DigitalFacultySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.DataService.Repositories.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>{}
    public interface IAcademicYearRepository : IGenericRepository<AcademicYear>{}
    public interface IDepartmentRepository : IGenericRepository<Department> { }

}
