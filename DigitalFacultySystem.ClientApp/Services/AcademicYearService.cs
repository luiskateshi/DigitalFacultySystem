using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using DigitalFacultySystem.Entities.Dtos.Requests;
using DigitalFacultySystem.Entities.Dtos.Responses;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DigitalFacultySystem.ClientApp.Services
{
    public class AcademicYearService : GenericService, IGenericService<AcademicYearDto>
    {
        public AcademicYearService(HttpClient http) : base(http)
        {
        }


        public async Task<bool> Add(AcademicYearDto academicYear)
        {
            try
            {
                var academicYearJson = new StringContent(JsonSerializer.Serialize(academicYear), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync("api/academicYear", academicYearJson);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to add academicYear");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/academicYear?id={id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to delete academicYear");
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<AcademicYearDto> GetById(Guid id)
        {
            try
            {
                var academicYear = await _http.GetFromJsonAsync<AcademicYearDto>($"api/academicYear?id={id}");
                return academicYear;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<AcademicYearDto?>> GetAll()
        {
            try
            {
                var apiResponse = await _http.GetStreamAsync("api/academicYear/all");
                var academicYears = await JsonSerializer.DeserializeAsync<List<AcademicYearDto>>(apiResponse, _SerializerOptions);
                return academicYears;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> Update(AcademicYearDto academicYear)
        {
            try
            {
                var academicYearJson = new StringContent(JsonSerializer.Serialize(academicYear), Encoding.UTF8, "application/json");
                var response = await _http.PutAsync("api/academicYear", academicYearJson);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to update academicYear");
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
