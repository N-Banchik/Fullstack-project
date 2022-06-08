using Classlibrery.DataAccessLayer.DTO_s;
using Classlibrery.DataAccessLayer.IRepository;
using Classlibrery.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LoginAndRegistration
{
    
    public class LoginController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> LoginAsync( LoginUserDto loginUserDto)
        {
            try
            {
                UserDto user = await _unitOfWork._accountRepository.Login(loginUserDto);
                await _unitOfWork.CompleteAsync();
                return Ok(user);
            }
            catch (BadRequestExtention ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedExtention ex)
            {
                return Unauthorized(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
