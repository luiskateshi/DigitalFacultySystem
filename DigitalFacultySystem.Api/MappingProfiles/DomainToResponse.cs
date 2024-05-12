using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;

namespace DigitalFacultySystem.Api.MappingProfiles
{
    public class DomainToResponse : AutoMapper.Profile
    {
        public DomainToResponse()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<AcademicYear, AcademicYearDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DegreeProgram, DegreeProgramDto>();   
            CreateMap<Lecturer, LecturerDto>();
            CreateMap<StudyPlan, StudyPlanDto>();
            CreateMap<Subject, SubjectDto>();
            CreateMap<Generation, GenerationDto>();
            CreateMap<Group, GroupDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<ExamsSession, ExamSessionDto>();
            CreateMap<StudentsInGroup, StudentInGroupDto>();
            CreateMap<StudyPlanSubject, StudyPlanSubjectDto>();
            CreateMap<Exam, ExamDto>();

                   
        }
    }
}
