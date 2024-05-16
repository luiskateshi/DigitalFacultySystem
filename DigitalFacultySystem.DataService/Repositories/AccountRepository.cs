using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Entities.DbSet;
using DigitalFacultySystem.Entities.Dtos.SecurityDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static DigitalFacultySystem.Entities.Dtos.SecurityDtos.ServiceResponses;

namespace DigitalFacultySystem.DataService.Repositories
{
    public class AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IUserAccount
    {

        public async Task<GeneralResponse> CreateAccount(UserDto userDTO)
        {
            if (userDTO is null)
                return new GeneralResponse(false, "Model is empty");

            // Control if role is what is expected
            if (userDTO.Role != "admin" && userDTO.Role != "staff" && userDTO.Role != "student")
                return new GeneralResponse(false, "Role is not valid");

            var newUser = new ApplicationUser()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
                return new GeneralResponse(false, "User already registered");

            var createUserResult = await userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUserResult.Succeeded)
                return new GeneralResponse(false, "Error occurred. Please try again");

            // Get the ID of the newly created user
            var userId = newUser.Id;

            // Check if the role specified in UserDto exists, if not, create it
            var roleExists = await roleManager.RoleExistsAsync(userDTO.Role);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = userDTO.Role });
            }

            // Assign the role specified in UserDto to the user
            await userManager.AddToRoleAsync(newUser, userDTO.Role);

            return new GeneralResponse(true, userId);
        }


        public async Task<LoginResponse> LoginAccount(LoginDto loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login completed");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<GeneralResponse> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var Id = userId.ToString();
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
                return new GeneralResponse(false, "User not found");

            var passwordChangeResult = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!passwordChangeResult.Succeeded)
            {
                var errors = string.Join(", ", passwordChangeResult.Errors.Select(e => e.Description));
                return new GeneralResponse(false, $"Password change failed: {errors}");
            }

            return new GeneralResponse(true, "Password changed successfully");
        }
    }
}
