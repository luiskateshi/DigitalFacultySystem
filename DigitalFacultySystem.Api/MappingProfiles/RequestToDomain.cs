using AutoMapper;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DigitalFacultySystem.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<StudentDto, Student>();
            CreateMap<AcademicYearDto, AcademicYear>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<DegreeProgramDto, DegreeProgram>();
            CreateMap<LecturerDto, Lecturer>();
            CreateMap<StudyPlanDto, StudyPlan>();
            CreateMap<SubjectDto, Subject>();
            CreateMap<GenerationDto, Generation>();
            CreateMap<GroupDto, Group>();
            CreateMap<CourseDto, Course>();
            CreateMap<ExamSessionDto, ExamsSession>();
            CreateMap<StudentInGroupDto, StudentsInGroup>();
            CreateMap<StudyPlanSubjectDto, StudyPlanSubject>();
        }
    }
}
