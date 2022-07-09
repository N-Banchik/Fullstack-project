using API.HelpersClasses;
using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.DTOs.ViewDtos;
using DataAccess.ErrorHandling;
using DataAccess.Repository.IRepository;
using DataAccess.SearchParams;
using DataAccess.Services.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Guides
{

    public class GuidesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GuidesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<GuideDto>> GetGuide(int Id)
        {
            try
            {

                return Ok(await _unitOfWork._guideRepository.GetGuideById(Id));
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

        [HttpGet("Hobbies")]
        public async Task<ActionResult> GetGuidesForHobby(GuideSearchParams searchParams)
        {
            try
            {
                PaginatedList<GuideViewDto>? eventsDto = await _unitOfWork._guideRepository.GetGuidesForHobby(searchParams);
                Response.AddPaginationHeader(eventsDto.CurrentPage, eventsDto.PageSize, eventsDto.TotalCount, eventsDto.TotalPages);
                return Ok(eventsDto);
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
        [HttpGet("User")]
        public async Task<ActionResult> GetGuidesForUser(GuideSearchParams searchParams)
        {
            try
            {
                PaginatedList<GuideViewDto>? eventsDto = await _unitOfWork._guideRepository.GetGuidesForUser(searchParams);
                Response.AddPaginationHeader(eventsDto.CurrentPage, eventsDto.PageSize, eventsDto.TotalCount, eventsDto.TotalPages);
                return Ok(eventsDto);
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
        public async Task<ActionResult> CreateGuide(GuideCreationDto creationDto)
        {
            try
            {
                await _unitOfWork._guideRepository.CreateGuide(creationDto, User.GetUserId());
                await _unitOfWork.CompleteAsync();
                return NoContent();
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
        [HttpPut]
        public async Task<ActionResult> UpdateGuide( GuideUpdateDto updateDto)
        {
            try
            {
                List<string> userRoles = User.GetUserRole();
                await _unitOfWork._guideRepository.EditGuide(updateDto, User.GetUserId(), userRoles.Contains("Admin"));
                await _unitOfWork.CompleteAsync();
                return NoContent();
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
