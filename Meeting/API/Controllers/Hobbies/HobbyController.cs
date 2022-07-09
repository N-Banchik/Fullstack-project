using API.HelpersClasses;
using DataAccess.Repository.IRepository;
using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Services.Pagination;
using DataAccess.SearchParams;
using DataAccess.DTOs.ViewDtos;

namespace API.Controllers.Hobbies
{

    public class HobbyController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public HobbyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> AddHobbyAsync(HobbyCreationDto CreationDto)
        {
            try
            {
                int userId = User.GetUserId();
                await _unitOfWork._hobbyRepository.AddAsync(CreationDto, userId);
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
        public async Task<ActionResult> GetAllHobbies([FromQuery] HobbySearchParams searchParams)
        {
            try
            {
                PaginatedList<HobbyViewDto>? eventsDto = await _unitOfWork._hobbyRepository.GetAllAsync(searchParams);
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
        [HttpGet("{id}",Name = "GetHobbyById")]
        public async Task<ActionResult<HobbyDto>> GetHobbyById(int id)
        {
            try
            {
                return Ok(await _unitOfWork._hobbyRepository.GetById(id));
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
        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<HobbyDto>> UpdateCategories(UpdateHobbyCategoryDto update)
        {
            try
            {
                await _unitOfWork._hobbyRepository.UpdateHobbyCategory(update);
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

        [HttpPut("UpdateHobby")]
        public async Task<ActionResult<HobbyDto>> UpdateHobby(HobbyUpdateDto update)
        {
            try
            {
                await _unitOfWork._hobbyRepository.EditHobby(update);
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

        [HttpGet("FindByFitures")]
        public async Task<ActionResult> GetHobbiesByKeys([FromQuery] HobbySearchParams searchParams)
        {
            try
            {
                PaginatedList<HobbyViewDto>? eventsDto = await _unitOfWork._hobbyRepository.GetHobbiesByKeys(searchParams);
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
        [HttpGet("Category", Name = "GetHobbiesByCategory")]
        public async Task<ActionResult<IEnumerable<HobbyDto>>> GetHobbiesByCategory([FromQuery] HobbySearchParams searchParams)
        {
            try
            {
                PaginatedList<HobbyViewDto>? eventsDto = await _unitOfWork._hobbyRepository.GetHobbiesByCategory(searchParams);
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

        [HttpPost("Follow/{hobbyid}")]
        public async Task<ActionResult<HobbyDto>> FollowHobby(int hobbyid)
        {
            try
            {
                int userId = User.GetUserId();
                await _unitOfWork._hobbyRepository.StartFollowHobby(userId,hobbyid);
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
        [HttpPost("UnFollow/{hobbyid}")]
        public async Task<ActionResult<HobbyDto>> UnFollowHobby(int hobbyid)
        {
            try
            {
                int userId = User.GetUserId();
                await _unitOfWork._hobbyRepository.StopFollowHobby(userId, hobbyid);
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

        [HttpGet("Followed")]
        public async Task<ActionResult> GetFollowedHobbies(HobbySearchParams searchParams)
        {
            try
            {
                PaginatedList<HobbyViewDto>? eventsDto = await _unitOfWork._hobbyRepository.GetFollowedHobbies(searchParams);
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

        [HttpPost("Photo")]
        public async Task<ActionResult<HobbyDto>> AddPhoto(IFormFile file, [FromQuery] int HobbyId)
        {
            try
            {
                
                int userId = User.GetUserId();
                await _unitOfWork._hobbyRepository.AddPhoto(file, HobbyId,userId);
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

        [HttpGet("{hobbyId}/members")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetHobbyMembers(int hobbyId)
        {
            try
            {
                IEnumerable<MemberDto> users = await _unitOfWork._hobbyRepository.GetHobbyUsers(hobbyId);
                return Ok(users);
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
