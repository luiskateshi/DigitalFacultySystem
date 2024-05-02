﻿using DigitalFacultySystem.Domain.Entities;
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



        }
    }
}
