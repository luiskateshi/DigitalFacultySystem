using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Entities.Dtos.SecurityDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDTO)
        {
            var response = await userAccount.CreateAccount(userDTO);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDTO)
        {
            var response = await userAccount.LoginAccount(loginDTO);
            return Ok(response);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePassDto changePasswordDto)
        {
            var response = await userAccount.ChangePassword(changePasswordDto.UserId, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            return Ok(response);
        }
    }
}
