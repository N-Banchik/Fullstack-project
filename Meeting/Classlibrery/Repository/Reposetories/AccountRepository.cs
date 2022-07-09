using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Repository.IRepository;
using DataAccess.DTOs;
using DataAccess.ErrorHandling;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccess.DTOs.UpdateDtos;
using AutoMapper;

namespace DataAccess.Repository.Reposetories
{
    public class AccountRepository : BaseRepository<User, UserDto>, IAccountRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountRepository(DataContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, ITokenService TokenService,IMapper mapper) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

            _tokenService = TokenService;
            _mapper = mapper;
        }

        public async Task<UserDto> ChangePassword(PasswordChangeDto changeDto,int userId )
        {
            if (changeDto.CurrentPassword==changeDto.NewPassword)
            {
                throw new BadRequestExtention(ErrorMessages.PasswordSameAsOld);
            }
            User? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new BadRequestExtention(ErrorMessages.UserNotFound);
            }
            IdentityResult? result = await _userManager.ChangePasswordAsync(user, changeDto.CurrentPassword, changeDto.NewPassword);
            if (result.Succeeded)
            {
                return _mapper.Map<UserDto>(user);
            }
            else
            {
                throw new BadRequestExtention(result.Errors.FirstOrDefault()?.Description ?? ErrorMessages.PasswordChangeFailed);
            }
        }

        public async Task<UserDto> Login(LoginUserDto loginUser)
        {

            User user = await _userManager.FindByEmailAsync(loginUser.Email);

            if (user == null) throw new UnauthorizedExtention(ErrorMessages.InvalidEmailPassword);
            if (await _userManager.IsLockedOutAsync(user)) throw new UnauthorizedExtention(ErrorMessages.LockedOut);
            SignInResult result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, false, false);
            if (result.Succeeded)
            {
                string token = await _tokenService.CreateTokenAsync(user);
                return new UserDto
                {
                    Username = user.UserName,
                    Token = token,
                    Id=user.Id
                };


            }
            else
            {
                throw new UnauthorizedExtention(ErrorMessages.InvalidEmailPassword);
            }


        }

        public async Task<UserDto> Register(RegisterUserDto registerUser)
        {
            //If email already exists => throw.
            if (await this.UserExists(registerUser.Email)) throw new BadRequestExtention(ErrorMessages.EmailInUse);


            //Create user.
            User newUser = new()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                FullName = registerUser.FirstName + " " + registerUser.LastName,
                Country = registerUser.Country,
                City = registerUser.City
            };
            try
            {

                IdentityResult result = await _userManager.CreateAsync(newUser, registerUser.Password);
                if (!result.Succeeded) throw new Exception($"Internal error {result.Errors.First().Description}");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + "" + ex.InnerException?.Message);
            }

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

