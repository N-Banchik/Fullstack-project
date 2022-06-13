using DataAccess.Data.Entities;

namespace DataAccess.Services.Interfaces
{
    public interface ITokenService
    {
         Task<string> CreateTokenAsync(User user);
         string CreateToken(User user,string role);
    }
}