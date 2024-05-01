using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.Requests;
using DigitalFacultySystem.Entities.Dtos.Responses;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace DigitalFacultySystem.ClientApp.Services
{
    public class GenericService
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
    }
}
