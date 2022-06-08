using Classlibrery.DataAccessLayer.DTO_s;
using Classlibrery.DataAccessLayer.IRepository;
using Classlibrery.ErrorHandling;
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
                return Ok();
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

        [HttpGet("get")]
        public string get() { return "hello"; }
    }
}
