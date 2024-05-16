using Blazored.LocalStorage;
using DigitalFacultySystem.Client.Authentication;
using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using DigitalFacultySystem.Entities.Dtos.SecurityDtos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static DigitalFacultySystem.Entities.Dtos.SecurityDtos.ServiceResponses;

namespace DigitalFacultySystem.Client.Services
{
    public class GenericService<TDto> : IGenericService<TDto> where TDto : class
    {
        protected readonly HttpClient _http;
        protected readonly JsonSerializerOptions _SerializerOptions;
        private readonly ILocalStorageService localStorageService;

        public GenericService(HttpClient http, ILocalStorageService localStorageService)
        {
            _http = http;
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            this.localStorageService = localStorageService;
        }

        public async Task<bool> Add(TDto entity, string apiUrl)
        {
            try
            {
                string? token = await localStorageService.GetItemAsStringAsync("token");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var entityJson = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync(apiUrl, entityJson);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
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
                string? token = await localStorageService.GetItemAsStringAsync("token");
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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
                string? token = await localStorageService.GetItemAsStringAsync("token");
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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

                string? token = await localStorageService.GetItemAsStringAsync("token");
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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



        public async Task<List<TDto>> GetAllById(Guid? id, string apiUrl)
        {
            try
            {
                string? token = await localStorageService.GetItemAsStringAsync("token");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var apiResponse = await _http.GetStreamAsync($"{apiUrl}?id={id}");


                var entities = await JsonSerializer.DeserializeAsync<List<TDto>>(apiResponse, _SerializerOptions);


                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(TDto entity, string apiUrl)
        {
            try
            {
                string? token = await localStorageService.GetItemAsStringAsync("token");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        public async Task<bool> UpdateList(IEnumerable<TDto> entities, string apiUrl)
        {
            try
            {
                string? token = await localStorageService.GetItemAsStringAsync("token");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var entitiesJson = new StringContent(JsonSerializer.Serialize(entities), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync(apiUrl, entitiesJson);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> ExecuteProcess(string apiUrl)
        {
            try
            {
                string? token = await localStorageService.GetItemAsStringAsync("token");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _http.PostAsync(apiUrl, null);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> ExecuteProcessById(Guid Id, string apiUrl)
        {
            try
            {
                string? token = await localStorageService.GetItemAsStringAsync("token");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _http.PostAsync($"{apiUrl}?id={Id}", null);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Guid> GetStudentIdByUserId(Guid Id, string apiUrl)
        {
            try
            {
                string? token = await localStorageService.GetItemAsStringAsync("token");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _http.GetStreamAsync($"{apiUrl}?id={Id}");
                var studentId = await JsonSerializer.DeserializeAsync<string>(response, _SerializerOptions);
                return Guid.Parse(studentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


    }
}
