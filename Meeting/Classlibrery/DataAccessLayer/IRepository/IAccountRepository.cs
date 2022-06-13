using DataAccess.DataAccessLayer.DTO_s;

namespace DataAccess.DataAccessLayer.IRepository
{
    public interface IAccountRepository
    {
        Task<UserDto> Register(RegisterUserDto user);
        Task<UserDto> Login(LoginUserDto user);
        Task<bool> UserExists(string Email);
    }
}
