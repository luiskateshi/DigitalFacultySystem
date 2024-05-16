using DigitalFacultySystem.Entities.Dtos.SecurityDtos;
using static DigitalFacultySystem.Entities.Dtos.SecurityDtos.ServiceResponses;

namespace DigitalFacultySystem.DataService.Repositories.Interfaces
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserDto userDto);
        Task<LoginResponse> LoginAccount(LoginDto loginDto);
        Task<GeneralResponse> ChangePassword(string userId, string currentPassword, string newPassword);
    }
}
