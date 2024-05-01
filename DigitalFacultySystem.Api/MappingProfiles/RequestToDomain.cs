using AutoMapper;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using DigitalFacultySystem.Entities.Dtos.Requests;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DigitalFacultySystem.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            //add 1 year to the birthdate and isActive = true
            CreateMap<CreateStudentRequest, Student>()
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate.Value.AddYears(1)))
                .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.ExamRetakeRequests, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UpdateStudentRequest, Student>();
            CreateMap<AcademicYearDto, AcademicYear>();




        }
    }
}
