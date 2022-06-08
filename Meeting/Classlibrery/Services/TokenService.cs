using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Classlibrery.Data.Entities;
using Classlibrery.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Classlibrery.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;
        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            List<Claim> claims = new List<Claim> {
              new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
              new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
            SigningCredentials creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(28),
                SigningCredentials = creds,

            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string CreateToken(User user,string role)
        {
            List<Claim> claims = new List<Claim> {
              new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
              new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            claims.Add(new Claim(ClaimTypes.Role, role));
            SigningCredentials creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(28),
                SigningCredentials = creds,

            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}