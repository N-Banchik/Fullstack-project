using Classlibrery.Data.Entities;

namespace Classlibrery.Services.Interfaces
{
    public interface ITokenService
    {
         Task<string> CreateTokenAsync(User user);
         string CreateToken(User user,string role);
    }
}