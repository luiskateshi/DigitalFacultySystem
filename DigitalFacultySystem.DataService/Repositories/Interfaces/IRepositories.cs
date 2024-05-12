using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.DataService.Repositories.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>{ }

    public interface IAcademicYearRepository : IGenericRepository<AcademicYear>{ }

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

    public interface ICourseRepository : IGenericRepository<Course> {
        Task<Course> GenerateCourseFromStudyPlanSubject(StudyPlanSubject studyPlanSubject);
        Task<bool> GenerateStudentsInCourse();
        Task<IEnumerable<CourseAttendanceDto>> GetStudentsInCourse(Guid courseId);
        Task<bool> UpdateCourseAttendance(IEnumerable<CourseAttendanceDto> courseAttendanceDtos);
        Task<bool> CalculateCourseAttendance(Guid courseId);
    }

    public interface IExamsSessionRepository : IGenericRepository<ExamsSession> {
        Task<bool> GenerateExamsAndStudentsInExams(Guid examSessionId);
        Task<bool> EndExamSession(Guid examSessionId);
    }

    public interface IStudentInGroupRepository : IGenericRepository<StudentsInGroup> {
        Task<IEnumerable<Student>> GetStudentsByGroup(Guid groupId);
        Task<IEnumerable<Student>> GetStudentsWithNoGroup(Guid degreeId);
    }

    public interface IStudyPlanSubjectRepository : IGenericRepository<StudyPlanSubject>
    {
        Task<IEnumerable<StudyPlanSubject>> GetSubjectsByStudyPlan(Guid studyPlanId);
        Task<IEnumerable<Subject>> GetSubjectsNotInStudyPlan(Guid studyPlanId);
    }

    public interface IExamRepository : IGenericRepository<Exam> {
        Task<IEnumerable<ExamDto>> GetExamsByExamSession(Guid examSessionId);
        Task<IEnumerable<StudentsInExamDto>> GetStudentsInExam(Guid examId);
        Task<bool> UpdateStudentsInExam(IEnumerable<StudentsInExamDto> studentsInExam);
    }

    public interface IExamRetakeRequestRepository : IGenericRepository<ExamRetakeRequest>
    {
        Task<IEnumerable<ExamRetakeRequestDto>> GetRequestsByStudent(Guid studentId);
        Task<IEnumerable<PossibleExamRetakesDto>> GetPossibleExamRetakes(Guid studentId);
    }


}
