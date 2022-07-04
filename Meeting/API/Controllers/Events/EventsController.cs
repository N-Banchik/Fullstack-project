using API.HelpersClasses;
using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.DTOs.ViewDtos;
using DataAccess.ErrorHandling;
using DataAccess.Repository.IRepository;
using DataAccess.SearchParams;
using DataAccess.Services.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Events
{

    public class EventsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvent(EventCreationDto creationDto)
        {
            try
            {
                int userId = User.GetUserId();
                await _unitOfWork._eventsRepository.CreateEvent(creationDto, userId);
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

        [HttpPut]
        public async Task<ActionResult> EditEvent(EventUpdateDto updateDto)
        {
            try
            {

                await _unitOfWork._eventsRepository.EditEvent(updateDto);
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

        [HttpPut("Cancel/{eventId}")]
        public async Task<ActionResult> CancelEvent(int eventId)
        {
            try
            {

                await _unitOfWork._eventsRepository.CancelEvent(eventId);
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

        [HttpGet("{eventId}")]
        public async Task<ActionResult> GetEvent(int eventId)
        {
            try
            {
                var eventDto = await _unitOfWork._eventsRepository.GetEventById(eventId);
                return Ok(eventDto);
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

        [HttpGet("Member/Events")]
        public async Task<ActionResult> GetEventsForMember([FromQuery] EventSearchParams searchParams)
        {
            try
            {
                PaginatedList<EventViewDto>? eventsDto = await _unitOfWork._eventsRepository.GetEventsForMember(searchParams);
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
        [HttpGet]
        public async Task<ActionResult> GetAllEvents([FromQuery] EventSearchParams searchParams)
        {
            try
            {
                PaginatedList<EventViewDto>? eventsDto = await _unitOfWork._eventsRepository.GetAllEvents(searchParams);
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
        [HttpGet("Hobby")]
        public async Task<ActionResult> GetAllEventsForHobby([FromQuery] EventSearchParams searchParams)
        {
            try
            {
                PaginatedList<EventViewDto>? eventsDto = await _unitOfWork._eventsRepository.GetAllEventsForHobby(searchParams);
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
        [HttpGet("EventName")]
        public async Task<ActionResult> GetEventByName([FromQuery] EventSearchParams searchParams)
        {
            try
            {
                PaginatedList<EventViewDto>? eventsDto = await _unitOfWork._eventsRepository.GetEventsByName(searchParams);
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

        [HttpGet("Creator")]
        public async Task<ActionResult> GetEventsCreatedByUser([FromQuery] EventSearchParams searchParams, int userId)
        {
            try
            {
                PaginatedList<EventViewDto>? eventsDto = await _unitOfWork._eventsRepository.GetAllEventsCreatedByUser(searchParams);
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

        [HttpGet("Members")]
        public async Task<ActionResult> GetMembersForEvent(int eventId)
        {
            try
            {
                IEnumerable<EventMemberDto> membersDto = await _unitOfWork._eventsRepository.GetMembersForEvent(eventId);
                return Ok(membersDto);
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

        [HttpPost("Comment")]
        public async Task<ActionResult> CreateComment(PostCreationDto creationDto)
        {
            try
            {
                int userId = User.GetUserId();
                await _unitOfWork._eventsRepository.CreatePost(creationDto, userId);
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

        [HttpPut("Comment")]
        public async Task<ActionResult> UpdateComment(PostUpdateDto updateDto)
        {
            try
            {
                await _unitOfWork._eventsRepository.EditPost(updateDto);
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

        [HttpDelete("Comment/{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            try
            {

                await _unitOfWork._eventsRepository.DeletePost(commentId);
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

        [HttpGet("Comment/{eventId}")]
        public async Task<ActionResult> GetComments(int eventId)
        {
            try
            {
                IEnumerable<DataAccess.DTOs.PostDto>? commentsDto = await _unitOfWork._eventsRepository.GetPostsForEvent(eventId);
                return Ok(commentsDto);
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

        [HttpGet("Comments/{userId}")]
        public async Task<ActionResult> GetCommentsForMember(int userId)
        {
            try
            {
                IEnumerable<DataAccess.DTOs.PostDto>? commentsDto = await _unitOfWork._eventsRepository.GetPostsForMember(userId);
                return Ok(commentsDto);
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

        [HttpPost("RSVP/{eventId}")]
        public async Task<ActionResult> AddRegisterToEvent(int eventId)
        {
            try
            {
                int userId = User.GetUserId();
                await _unitOfWork._eventsRepository.RegisterToEvent(eventId, userId);
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

        [HttpPost("Attend/{eventId}")]
        public async Task<ActionResult> ChangeAttendingToEvent(int eventId)
        {
            try
            {
                int userId = User.GetUserId();
                bool arriving = await _unitOfWork._eventsRepository.ChangeAttending(eventId, userId);
                await _unitOfWork.CompleteAsync();
                return Ok(arriving);
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
