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
    public interface IDegreeProgramRepository : IGenericRepository<DegreeProgram> { }
    public interface ILecturerRepository : IGenericRepository<Lecturer> { }
    public interface IStudyPlanRepository : IGenericRepository<StudyPlan> { }
    public interface ISubjectRepository : IGenericRepository<Subject> {
        //custom method to get all subjects for a specific studyplan
        Task<IEnumerable<Subject>> AllForStudyPlan(Guid studyPlanId);
    }
    public interface IGenerationRepository : IGenericRepository<Generation> { }
    public interface IGroupRepository : IGenericRepository<Group> { }
    public interface ICourseRepository : IGenericRepository<Course> { }


}
