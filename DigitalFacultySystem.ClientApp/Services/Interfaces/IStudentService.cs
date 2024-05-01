using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.Requests;
using DigitalFacultySystem.Entities.Dtos.Responses;

namespace DigitalFacultySystem.ClientApp.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentResponse>> GetStudents();
        Task<StudentResponse> GetStudent(Guid id);
        Task<bool> AddStudent(CreateStudentRequest student);
        Task<bool> DeleteStudent(Guid id);
        Task<bool> UpdateStudent(UpdateStudentRequest student);

    }
}
