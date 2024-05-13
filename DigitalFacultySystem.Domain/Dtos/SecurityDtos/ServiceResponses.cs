
namespace DigitalFacultySystem.Entities.Dtos.SecurityDtos
{
    public class ServiceResponses
    {
        public record class GeneralResponse(bool Flag, string Message);
        public record class LoginResponse(bool Flag, string Token, string Message);
    }
}
