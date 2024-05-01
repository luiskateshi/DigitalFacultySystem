using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using DigitalFacultySystem.Entities.Dtos.Responses;

namespace DigitalFacultySystem.Api.MappingProfiles
{
    public class DomainToResponse : AutoMapper.Profile
    {
        public DomainToResponse()
        {
            CreateMap<Student, StudentResponse>();
            CreateMap<AcademicYear, AcademicYearDto>();


        }
    }
}
