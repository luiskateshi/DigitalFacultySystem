using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.Requests;
using DigitalFacultySystem.Entities.Dtos.Responses;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DigitalFacultySystem.ClientApp.Services
{
    public class StudentService : GenericService, IStudentService
    {
        public StudentService(HttpClient http) : base(http)
        {
        }
        public async Task<bool> AddStudent(CreateStudentRequest student)
        {
            try
            {
                var studentJson = new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync("api/student", studentJson);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to add student");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/student?id={id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to delete student");
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<StudentResponse> GetStudent(Guid id)
        {
            try
            {
                var student = await _http.GetFromJsonAsync<StudentResponse>($"api/student?id={id}");
                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<StudentResponse?>> GetStudents()
        {
            try
            {
                var apiResponse = await _http.GetStreamAsync("api/student/all");
                var students = await JsonSerializer.DeserializeAsync<List<StudentResponse>>(apiResponse, _SerializerOptions);
                return students;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateStudent(UpdateStudentRequest student)
        {
            try
            {
                var studentJson = new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json");
                var response = await _http.PutAsync("api/student", studentJson);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to update student");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
