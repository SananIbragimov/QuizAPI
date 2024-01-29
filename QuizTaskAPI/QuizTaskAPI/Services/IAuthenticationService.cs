using QuizTaskAPI.DTOs.Account;

namespace QuizTaskAPI.Services
{
    public interface IAuthenticationService
    {
        public string Login(LoginDto loginDto);
        public void Register(RegisterDto registerDto);
    }
}
