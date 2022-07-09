using API.HelpersClasses;
using DataAccess.DTOs;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.ErrorHandling;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Members
{
    [Authorize]
    public class MembersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public MembersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPut("Location")]
        public async Task<ActionResult> UpdateLocation(MemberUpdateDto updateDto)
        {
            try
            {

                int userId = User.GetUserId();

                await _unitOfWork._memberRepository.UpdateLocation(updateDto, userId);
                await _unitOfWork.CompleteAsync();
                return Ok();
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
        [HttpGet]
        public async Task<ActionResult> GetMember()
        {
            try
            {
                int userId = User.GetUserId();
                MemberDto member = await _unitOfWork._memberRepository.GetMember(userId);
                return Ok(member);
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


        [HttpPost("Photo")]
        public async Task<ActionResult> AddPhoto(IFormFile file)
        {
            try
            {
                int userId = User.GetUserId();
                PhotoDto photo = await _unitOfWork._memberRepository.AddPhoto(file, userId);
                await _unitOfWork.CompleteAsync();
                return Ok(photo);
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

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(PasswordChangeDto changeDto)
        {
            try
            {
                int userId = User.GetUserId();
                UserDto user = await _unitOfWork._accountRepository.ChangePassword(changeDto, userId);
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
