using Classlibrery.DataAccessLayer.DTO_s;

namespace Classlibrery.DataAccessLayer.IRepository
{
    public interface IAccountRepository
    {
        Task<UserDto> Register(RegisterUserDto user);
        Task<UserDto> Login(LoginUserDto user);
        Task<bool> UserExists(string Email);
    }
}
