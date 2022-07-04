using DataAccess.Repository.IRepository;
using DataAccess.DTOs;
using DataAccess.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LoginAndRegistration
{

    public class RegistrationController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterUserDto registerUserDto)
        {
            try
            {
                UserDto user = await _unitOfWork._accountRepository.Register(registerUserDto);
                await _unitOfWork.CompleteAsync();
                return Ok(user);
            }
            catch (BadRequestExtention ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedExtention)
            {
                return Unauthorized();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }

        
    }
}
