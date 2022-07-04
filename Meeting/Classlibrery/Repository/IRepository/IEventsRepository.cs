using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.DTOs.ViewDtos;
using DataAccess.SearchParams;
using DataAccess.Services.Pagination;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IEventsRepository : IPostsRepository
    {
        Task CreateEvent(EventCreationDto creationDto, int userId);
        Task EditEvent(EventUpdateDto updateDto);
        Task CancelEvent(int eventId);
        Task<EventDto> GetEventById(int eventId);
        Task<PaginatedList<EventViewDto>> GetAllEvents(EventSearchParams searchParams);
        Task<PaginatedList<EventViewDto>> GetAllEventsForHobby(EventSearchParams searchParams);
        Task<PaginatedList<EventViewDto>> GetEventsByName(EventSearchParams searchParams);
        Task<PaginatedList<EventViewDto>> GetAllEventsCreatedByUser(EventSearchParams searchParams);
        Task<PaginatedList<EventViewDto>> GetEventsForMember(EventSearchParams searchParams);
        Task<List<EventMemberDto>> GetMembersForEvent(int eventId);
        Task<PhotoDto> AddPhoto(int eventId,IFormFile file);
        Task SetMainPhoto(int eventId, int photoId);
        Task DeletePhoto(int eventId, int photoId);
        Task RegisterToEvent(int eventId, int userId);
        Task<bool> ChangeAttending(int eventId, int userId);
        






    }
}
