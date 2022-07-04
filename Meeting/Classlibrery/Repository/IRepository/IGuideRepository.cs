using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.DTOs.ViewDtos;
using DataAccess.SearchParams;
using DataAccess.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IGuideRepository
    {
        Task CreateGuide(GuideCreationDto creationDto,int userId);
        Task EditGuide(GuideUpdateDto updateDto, int userId, bool isAdmin=false);
        Task<GuideDto> GetGuideById(int guideId);
        Task<PaginatedList<GuideViewDto>> GetGuidesForHobby(GuideSearchParams searchParams);
        Task<PaginatedList<GuideViewDto>> GetGuidesForUser(GuideSearchParams searchParams);

    }
}
