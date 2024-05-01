using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DigitalFacultySystem.ClientApp.Services
{
    public class GenericService<TDto> : IGenericService<TDto> where TDto : class
    {
        protected readonly HttpClient _http;
        protected readonly JsonSerializerOptions _SerializerOptions;

        public GenericService(HttpClient http)
        {
            _http = http;
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<bool> Add(TDto entity, string apiUrl)
        {
            try
            {
                var entityJson = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync(apiUrl, entityJson);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> Delete(Guid id, string apiUrl)
        {
            try
            {
                var response = await _http.DeleteAsync($"{apiUrl}?id={id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<TDto> GetById(Guid id, string apiUrl)
        {
            try
            {
                var entity = await _http.GetFromJsonAsync<TDto>($"{apiUrl}?id={id}");
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<TDto>> GetAll(string apiUrl)
        {
            try
            {
                var apiResponse = await _http.GetStreamAsync($"{apiUrl}/all");
                var entities = await JsonSerializer.DeserializeAsync<List<TDto>>(apiResponse, _SerializerOptions);
                return entities;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> Update(TDto entity, string apiUrl)
        {
            try
            {
                var entityJson = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
                var response = await _http.PutAsync(apiUrl, entityJson);
                response.EnsureSuccessStatusCode();
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
