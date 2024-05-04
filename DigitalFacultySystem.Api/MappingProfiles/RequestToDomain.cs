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
            //add 1 year to the birthdate and isActive = true
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
        }
    }
}
