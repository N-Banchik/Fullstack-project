using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.DataAccessLayer.DTO_s;
using DataAccess.DataAccessLayer.IRepository;
using DataAccess.ErrorHandling;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccessLayer.Reposetories
{
    public class AccountRepository : BaseRepository<User>, IAccountRepository
    {
        private readonly DataContext _context;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountRepository(DataContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, ITokenService TokenService) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

            _tokenService = TokenService;

        }


        public async Task<UserDto> Login(LoginUserDto loginUser)
        {
            User user = await _userManager.FindByEmailAsync(loginUser.Email);

            if (user == null) throw new UnauthorizedExtention("No User found with this Email and Password combination,Please Double chack Email and Password");
            if (await _userManager.IsLockedOutAsync(user)) throw new UnauthorizedExtention("Cannot sign-in right now.");
            SignInResult result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, false, false);
            if (result.Succeeded)
            {
                string token = await _tokenService.CreateTokenAsync(user);
                return new UserDto
                {
                    Username = user.UserName,
                    Token = token
                };


            }
            else
            {
                throw new UnauthorizedExtention("No User found with this Email and Password combination,Please Double chack Email and Password");
            }


        }

        public async Task<UserDto> Register(RegisterUserDto registerUser)
        {
            //If email already exists => throw.
            if (await this.UserExists(registerUser.Email)) throw new BadRequestExtention("Email already In Use");


            //Create user.
            User newUser = new()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, registerUser.Password);

            if (!result.Succeeded) throw new Exception($"Internal error {result.Errors.First().Description}");
            // _logger.LogInformation("Register failed",result.Errors);

            IdentityResult Role = await _userManager.AddToRoleAsync(newUser, "Member");

            if (!Role.Succeeded) throw new Exception($"Internal error {Role.Errors.First().Description}");

            return new UserDto()
            {

                Username = newUser.UserName,
                Token = _tokenService.CreateToken(newUser, "Member")

            };
        }

        public async Task<bool> UserExists(string Email)
        {
            try
            {
                //return true if user exists
                return await _context.Set<User>().Where(x => x.Email == Email).FirstOrDefaultAsync() == null ? false : true;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}

