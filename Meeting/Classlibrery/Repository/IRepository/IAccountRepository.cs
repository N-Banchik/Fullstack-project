using DataAccess.DTOs;
using DataAccess.DTOs.UpdateDtos;

namespace DataAccess.Repository.IRepository
{
    public interface IAccountRepository
    {
        Task<UserDto> Register(RegisterUserDto user);
        Task<UserDto> Login(LoginUserDto user);
        Task<bool> UserExists(string Email);
        Task<UserDto> ChangePassword(PasswordChangeDto changeDto,int userId);
    }
}
