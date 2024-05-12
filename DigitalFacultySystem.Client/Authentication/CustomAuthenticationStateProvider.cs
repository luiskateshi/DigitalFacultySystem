using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace DigitalFacultySystem.Client.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private  readonly ILocalStorageService _localStorageService;
        private ClaimsPrincipal anynomus = new ClaimsPrincipal(new ClaimsIdentity());
        
        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        //gets the authentication state from the local storage
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var authenticationModel = await _localStorageService.GetItemAsStringAsync("Authentication");
                if (authenticationModel == null)
                {
                    return await Task.FromResult(new AuthenticationState(anynomus));
                }
                return await Task.FromResult(new AuthenticationState(SetClaims(Deserialize(authenticationModel).Username!)));
            }
            catch 
            {
                return await Task.FromResult(new AuthenticationState(anynomus));
            }
        }

        //sets the token and username in the local storage (login)
        public async Task UpdateAuthenticationState(AuthenticationModel authenticationModel)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = new();
                if (authenticationModel != null)
                {
                    claimsPrincipal = SetClaims(authenticationModel.Username!);
                    await _localStorageService.SetItemAsync("Authentication", Serialize(authenticationModel));
                }
                else
                {                    
                    await _localStorageService.RemoveItemAsync("Authentication");
                    claimsPrincipal = anynomus;
                }
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
            catch
            {
                await Task.FromResult(new AuthenticationState(anynomus));
            }
        }

        private ClaimsPrincipal SetClaims(string email) => new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> 
        { 
            new Claim(ClaimTypes.Name, email) 
        }, "CustomAuth"));

        private AuthenticationModel Deserialize(string serializeString) => JsonSerializer.Deserialize<AuthenticationModel>(serializeString)!;
        private string Serialize(AuthenticationModel model) => JsonSerializer.Serialize(model);


    }
}
