using Blazored.LocalStorage;
using DigitalFacultySystem.Client.Authentication;
using DigitalFacultySystem.Client.Pages;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Entities.Dtos.SecurityDtos;
using System.Net.Http.Headers;
using static DigitalFacultySystem.Entities.Dtos.SecurityDtos.ServiceResponses;

namespace DigitalFacultySystem.Client.Services
{
    public class AccountService : IUserAccount
    {
        public AccountService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }
        private const string BaseUrl = "api/Account";
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public async Task<GeneralResponse> CreateAccount(UserDto userDTO)
        {
            var response = await httpClient
                 .PostAsync($"{BaseUrl}/register",
                 Generics.GenerateStringContent(
                 Generics.SerializeObj(userDTO)));

            //Read Response
            if (!response.IsSuccessStatusCode)
                return new GeneralResponse(false, "Error occured. Try again later...");

            var apiResponse = await response.Content.ReadAsStringAsync();
            return Generics.DeserializeJsonString<GeneralResponse>(apiResponse);
        }

        //change password
        public async Task<GeneralResponse> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var response = await httpClient
                .PostAsync($"{BaseUrl}/changePassword",
                               Generics.GenerateStringContent(
                                                  Generics.SerializeObj(new ChangePassDto() { UserId = userId, CurrentPassword = currentPassword, NewPassword = newPassword })));

            //Read Response
            if (!response.IsSuccessStatusCode)
                return new GeneralResponse(false, "Error occured. Try again later...");

            var apiResponse = await response.Content.ReadAsStringAsync();
            return Generics.DeserializeJsonString<GeneralResponse>(apiResponse);
        }



        public async Task<LoginResponse> LoginAccount(LoginDto loginDTO)
        {
            var response = await httpClient
               .PostAsync($"{BaseUrl}/login",
               Generics.GenerateStringContent(
               Generics.SerializeObj(loginDTO)));

            //Read Response
            if (!response.IsSuccessStatusCode)
                return new LoginResponse(false, null!, "Error occured. Try again later...");

            var apiResponse = await response.Content.ReadAsStringAsync();
            var result = Generics.DeserializeJsonString<LoginResponse>(apiResponse);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            var query = await httpClient.GetAsync("api/AcademicYear");
            return result;

        }


    }
}
