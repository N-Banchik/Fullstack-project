using DataAccess.Data.Entities;
using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.DTOs.ViewDtos;
using DataAccess.SearchParams;
using DataAccess.Services.Pagination;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Repository.IRepository
{
    public interface IHobbyRepository
    {
        Task AddAsync(HobbyCreationDto entity, int userId);
        Task<HobbyDto?> GetById(int id);
        void Update(Hobby entity);
        void Delete(Hobby entity);
        Task UpdateHobbyCategory(UpdateHobbyCategoryDto update);
        Task<PaginatedList<HobbyViewDto>> GetAllAsync(HobbySearchParams searchParams);
        Task<HobbyDto> EditHobby(HobbyUpdateDto hobby);
        Task<PaginatedList<HobbyViewDto>> GetHobbiesByKeys(HobbySearchParams searchParams);
        Task<PaginatedList<HobbyViewDto>> GetHobbiesByCategory(HobbySearchParams searchParams);
        Task<IEnumerable<MemberDto>> GetHobbyUsers(int HobbyId);
        Task StartFollowHobby(int userId, int hobbyId);
        Task StopFollowHobby(int userId, int hobbyId);
        Task<PaginatedList<HobbyViewDto>> GetFollowedHobbies(HobbySearchParams searchParams);
        Task<PhotoDto> AddPhoto(IFormFile file, int hobbyId, int userId);


    }

}
