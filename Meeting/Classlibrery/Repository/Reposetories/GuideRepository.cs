using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.DTOs.ViewDtos;
using DataAccess.ErrorHandling;
using DataAccess.Repository.IRepository;
using DataAccess.SearchParams;
using DataAccess.Services.Pagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Reposetories
{
    public class GuideRepository : BaseRepository<Guide, GuideDto>, IGuideRepository
    {


        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public GuideRepository(DataContext context, IMapper mapper, UserManager<User> userManager) : base(context)
        {

            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task CreateGuide(GuideCreationDto creationDto, int UserId)
        {
            if (creationDto.Content == string.Empty)
            {
                throw new BadRequestExtention(ErrorMessages.GuideWithNoContent);
            }
            Guide? guide = _mapper.Map<Guide>(creationDto);
            guide.CreatorId = UserId;
            await base._dbSet.AddAsync(guide);
        }

        public async Task EditGuide(GuideUpdateDto updateDto, int userId, bool isAdmin = false)
        {
            Guide? guide = await _dbSet.FindAsync(updateDto.Id);
            if (guide == null) throw new BadRequestExtention(ErrorMessages.GuideNotFound);
            if (guide.CreatorId != userId && !isAdmin) throw new UnauthorizedExtention(ErrorMessages.NotGuideCreator);
            guide.EditDate = DateTime.Now;
            guide.Content = updateDto.Content;
            guide.Title = updateDto.Title;
            Update(guide);
        }

        public async Task<GuideDto> GetGuideById(int guideId)
        {
            Guide? guide = await base._dbSet.Where(g => g.Id == guideId).Include(g => g.Creator).FirstOrDefaultAsync();
            if (guide == null)
            {
                throw new BadRequestExtention(ErrorMessages.GuideNotFound);
            }
            return _mapper.Map<GuideDto>(guide);
        }

        public async Task<PaginatedList<GuideViewDto>> GetGuidesForHobby(GuideSearchParams searchParams)
        {
            IQueryable<Guide> query = base._dbSet.Where(g => g.HobbyId == searchParams.Id);

            return await PaginatedList<GuideViewDto>
                .CreateAsync(query.ProjectTo<GuideViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);
        }
        

        public async Task<PaginatedList<GuideViewDto>> GetGuidesForUser(GuideSearchParams searchParams)
        {
            IQueryable<Guide> query = base._dbSet.Where(g => g.CreatorId == searchParams.Id);

            return await PaginatedList<GuideViewDto>
                .CreateAsync(query.ProjectTo<GuideViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);
        }
    }
}
