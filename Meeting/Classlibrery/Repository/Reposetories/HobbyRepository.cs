using AutoMapper;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Data.Entities.Bridge_Entities;
using DataAccess.Repository.IRepository;
using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.ErrorHandling;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet.Actions;
using DataAccess.DTOs.ViewDtos;
using DataAccess.Services.Pagination;
using DataAccess.SearchParams;
using AutoMapper.QueryableExtensions;

namespace DataAccess.Repository.Reposetories
{
    public class HobbyRepository : BaseRepository<Hobby, HobbyDto>, IHobbyRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly DbSet<UserHobby> _userHobbies;

        public HobbyRepository(DataContext context, UserManager<User> userManager, IMapper mapper, IPhotoService photoService) : base(context)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
            _userHobbies = _context.Set<UserHobby>();
        }


        public async Task AddAsync(HobbyCreationDto creationDto, int userId)
        {
            try
            {
                if (await base._dbSet.AnyAsync(x => x.HobbyName == creationDto.HobbyName))
                {
                    throw new BadRequestExtention(ErrorMessages.HobbExist);
                }
                Hobby hobby = _mapper.Map(creationDto, new Hobby());
                hobby.Users?.Add(new UserHobby() { User = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId) });
                base._dbSet.Add(hobby);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<HobbyDto> EditHobby(HobbyUpdateDto hobby)
        {
            try
            {
                Hobby? hobbyEntity = await base._dbSet.Include(p => p.Photo).FirstOrDefaultAsync();
                if (hobbyEntity == null)
                {
                    throw new BadRequestExtention(ErrorMessages.HobbyNotFound);
                }
                hobbyEntity.HobbyName = hobby.HobbyName;
                hobbyEntity.Description = hobby.Description;
                Update(hobbyEntity);
                return _mapper.Map<HobbyDto>(hobbyEntity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateHobbyCategory(UpdateHobbyCategoryDto update)
        {
            Hobby? HobbyToUpdate = await base._dbSet.FindAsync(update.HobbyId);
            if (HobbyToUpdate == null)
            {
                throw new BadRequestExtention(ErrorMessages.HobbyNotFound);
            }
            if (HobbyToUpdate.CategoryId == update.CategoryId)
                return;
            else
            {
                HobbyToUpdate.CategoryId = update.CategoryId;
                Update(HobbyToUpdate);
            }
        }

        public async Task<PaginatedList<HobbyViewDto>> GetAllAsync(HobbySearchParams searchParams)
        {
            IQueryable<Hobby>? query = base._dbSet.AsQueryable();
            return await PaginatedList<HobbyViewDto>
                .CreateAsync(query.ProjectTo<HobbyViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);
        }

        public new async Task<HobbyDto?> GetById(int id)
        {
            HobbyDto hobby = _mapper.Map<HobbyDto>(await base._dbSet.Where(x => x.Id == id).Include(p => p.Photo).Include(e => e.Events).Include(g => g.Guides).FirstOrDefaultAsync());
            if (hobby == null)
            {
                throw new BadRequestExtention(ErrorMessages.HobbyNotFound);
            }
            return hobby;
        }

        public async Task<IEnumerable<MemberDto>> GetHobbyUsers(int HobbyId)
        {
            List<User?> userList = await _context.UserHobbies!.Where(x => x.HobbyId == HobbyId).Include(u => u.User).Select(user => user.User).ToListAsync();
            if (userList == null)
            {
                throw new BadRequestExtention(ErrorMessages.HobbyNotFound);
            }
            return _mapper.Map<IEnumerable<MemberDto>>(userList);
        }

        public async Task<PaginatedList<HobbyViewDto>> GetHobbiesByKeys(HobbySearchParams searchParams)
        {
            IQueryable<Hobby>? query = base._dbSet.AsQueryable();
            query = query.Where(x => x.KeyFeatures!.Contains(searchParams.KeyFeatures!));
            return await PaginatedList<HobbyViewDto>
                     .CreateAsync(query.ProjectTo<HobbyViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PaginatedList<HobbyViewDto>> GetHobbiesByCategory(HobbySearchParams searchParams)
        {
            IQueryable<Hobby>? query = base._dbSet.AsQueryable();
            query = query.Where(x => x.CategoryId == searchParams.CategoryId);
            return await PaginatedList<HobbyViewDto>
                .CreateAsync(query.ProjectTo<HobbyViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task StartFollowHobby(int userId, int hobbyId)
        {
            UserHobby? userHobby = await _userHobbies.FirstOrDefaultAsync(x => x.HobbyId == hobbyId && x.UserId == userId);


            if (userHobby != null)
            {
                if (userHobby.Following == false)
                {
                    userHobby.Following = true;
                    _userHobbies.Update(userHobby);
                }
                else
                {
                    throw new BadRequestExtention(ErrorMessages.AlreadyFollow);
                }
            }
            else
            {
                userHobby = new UserHobby() { UserId = userId, HobbyId = hobbyId, Following = true };
                _userHobbies.Add(userHobby);
            }


        }

        public async Task StopFollowHobby(int userId, int hobbyId)
        {
            UserHobby? userHobby = await _userHobbies.FirstOrDefaultAsync(x => x.HobbyId == hobbyId && x.UserId == userId);
            if (userHobby == null)
            {
                throw new BadRequestExtention(ErrorMessages.HobbyNotFound);
            }
            userHobby.Following = false;
            _userHobbies.Update(userHobby);
        }

        public async Task<PaginatedList<HobbyViewDto>> GetFollowedHobbies(HobbySearchParams searchParams)
        {
            IQueryable<Hobby?> query = _userHobbies.AsQueryable()
            .Where(x => x.UserId == searchParams.UserId && x.Following == true).Include(h => h.Hobby).Select(h => h.Hobby);
            return await PaginatedList<HobbyViewDto>
                .CreateAsync(query.ProjectTo<HobbyViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);

        }

        public async Task<PhotoDto> AddPhoto(IFormFile file,int hobbyId, int userId)
        {
            Hobby? hobby = await base._dbSet.Include(p=>p.Photo).FirstOrDefaultAsync(x => x.Id == hobbyId);
            if (hobby == null)
            {
                throw new BadRequestExtention(ErrorMessages.HobbyNotFound);
            }
            ImageUploadResult result = await _photoService.UploadPhotoAsync(file);
            if (result.Error != null)
            {
                throw new BadRequestExtention(ErrorMessages.PhotoUploadFailed);
            };
            await DeletePhoto(hobby.Photo!.PublicId!);
            Photo<Hobby> photo = new Photo<Hobby>()
            {
                PhotoUrl = result.SecureUrl.AbsoluteUri.ToString(),
                PublicId = result.PublicId,
                
                UploaderID = userId
            };
            hobby.Photo = photo;
            Update(hobby);
            return _mapper.Map<PhotoDto>(photo);

        }

        internal async Task DeletePhoto(string PublicId)
        {
            DeletionResult result = await _photoService.DeletePhotoAsync(PublicId);
            if (result.Result == "ok")
            {
                return;

            }
            throw new Exception(result.Error.Message);
        }




    }
}

