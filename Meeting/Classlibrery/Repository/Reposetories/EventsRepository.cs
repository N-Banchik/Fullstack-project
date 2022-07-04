using AutoMapper;
using AutoMapper.QueryableExtensions;
using CloudinaryDotNet.Actions;
using DataAccess.Data.Entities;
using DataAccess.Data.Entities.Bridge_Entities;
using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.DTOs.ViewDtos;
using DataAccess.ErrorHandling;
using DataAccess.Repository.IRepository;
using DataAccess.SearchParams;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.Reposetories
{
    public class EventsRepository : BaseRepository<Event, EventDto>, IEventsRepository, IPostsRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IPhotoService _photoService;
        private readonly DbSet<Post> _postSet;
        private readonly DbSet<UserEvent> _userEvents;
        private readonly DbSet<Photo<Event>> _photos;

        public EventsRepository(DbContext dbContext, IMapper mapper, UserManager<User> userManager, IPhotoService photoService) : base(dbContext)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._photoService = photoService;
            this._postSet = dbContext.Set<Post>();
            this._userEvents = dbContext.Set<UserEvent>();
            this._photos = dbContext.Set<Photo<Event>>();
        }

        public async Task CancelEvent(int eventId)
        {
            Event? eventToCencel = await base.GetById(eventId);
            if (eventToCencel == null)
            {
                throw new BadRequestExtention(ErrorMessages.EventNotFound);
            }
            eventToCencel.Canceled = true;
            Update(eventToCencel);
        }

        public async Task CreateEvent(EventCreationDto creationDto, int userId)
        {
            Event eventToCreate = _mapper.Map<Event>(creationDto);
            User Creator = await _userManager.FindByIdAsync(userId.ToString());
            eventToCreate.Creator = Creator;
            _userEvents.Add(new UserEvent() { Event = eventToCreate, User = Creator });
            base.Add(eventToCreate);
        }

        public async Task EditEvent(EventUpdateDto updateDto)
        {
            Event? eventToEdit = await base.GetById(updateDto.Id);
            if (eventToEdit == null)
            {
                throw new BadRequestExtention(ErrorMessages.EventNotFound);
            }
            eventToEdit.EventDate = updateDto.EventDate;
            eventToEdit.EventDescription = updateDto.EventDescription;
            eventToEdit.EventLocation = updateDto.EventLocation;
            eventToEdit.EventTitle = updateDto.EventTitle;
            eventToEdit.EventRules = updateDto.EventRules;
            Update(eventToEdit);
        }

        public async Task<PaginatedList<EventViewDto>> GetAllEvents(EventSearchParams searchParams)
        {
            IQueryable<Event>? query = base._dbSet.AsQueryable();
            query.Where(e => e.Canceled == false);
            query = query.Where(e => e.EventDate.Date >= searchParams.Date.Date);
            query = searchParams.OrderBy switch
            {
                "created" => query.OrderBy(u => u.EventCreated),
                _ => query.OrderBy(e => e.EventDate),
            };
            return await PaginatedList<EventViewDto>
                .CreateAsync(query.ProjectTo<EventViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);





        }
        public async Task<PaginatedList<EventViewDto>> GetAllEventsForHobby(EventSearchParams searchParams)
        {
            IQueryable<Event>? query = base._dbSet.AsQueryable();
            if (searchParams.HobbyId == 0) throw new BadRequestExtention(ErrorMessages.HobbyNotFound);
            query.Where(e => e.HobbyId == searchParams.HobbyId);
            query.Where(e => e.Canceled == false);
            query = query.Where(e => e.EventDate.Date >= searchParams.Date.Date);
            query = searchParams.OrderBy switch
            {
                "created" => query.OrderBy(u => u.EventCreated),
                _ => query.OrderBy(e => e.EventDate),
            };
            return await PaginatedList<EventViewDto>
                .CreateAsync(query.ProjectTo<EventViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);

        }
        public async Task<PaginatedList<EventViewDto>> GetEventsByName(EventSearchParams searchParams)
        {
            IQueryable<Event>? query = base._dbSet.AsQueryable();
            query.Where(e => e.Canceled == false);
            query = query.Where(e => e.EventTitle!.Contains(searchParams.EventName!));
            query = query.OrderBy(e => e.EventDate);
            return await PaginatedList<EventViewDto>
                .CreateAsync(query.ProjectTo<EventViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PaginatedList<EventViewDto>> GetAllEventsCreatedByUser(EventSearchParams searchParams)
        {
            IQueryable<Event>? query = base._dbSet.AsQueryable();
            query = query.Where(e => e.Canceled == false);
            query = query.Where(e => e.EventCreatorId == searchParams.UserId);
            query = query.OrderBy(e => e.EventDate);
            return await PaginatedList<EventViewDto>.CreateAsync(query.ProjectTo<EventViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);

        }

        public async Task<EventDto> GetEventById(int eventId)
        {
            Event? eventToReturn = await base._dbSet.Where(e => e.Id == eventId).Include(p => p.Photos).Include(p => p.Posts!.Where(p => p.Deleted == false)).FirstOrDefaultAsync();
            if (eventToReturn == null)
            {
                throw new BadRequestExtention(ErrorMessages.EventNotFound);
            };
            EventDto eventDto = _mapper.Map<EventDto>(eventToReturn);
            eventDto.Users = await GetMembersForEvent(eventToReturn.Id);

            return _mapper.Map<EventDto>(eventDto); ;
        }
        public async Task<PaginatedList<EventViewDto>> GetEventsForMember(EventSearchParams searchParams)
        {
            IQueryable<Event>? query = _userEvents.AsQueryable()
            .Where(e => e.UserId == searchParams.UserId)
            .Include(e => e.Event).ThenInclude(x => x!.Photos).Select(e => e.Event!).OrderBy(e => e.EventDate);


            return await PaginatedList<EventViewDto>
                            .CreateAsync(query.ProjectTo<EventViewDto>(_mapper.ConfigurationProvider).AsNoTracking(), searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task CreatePost(PostCreationDto creationDto, int userId)
        {
            Post newPost = new() { CreatorId = userId, Content = creationDto.PostContent, EventId = creationDto.EventId };
            Event? toAdd = await _dbSet.FirstOrDefaultAsync(e => e.Id == creationDto.EventId);
            if (toAdd == null)
            {
                throw new BadRequestExtention(ErrorMessages.EventNotFound);
            }
            await _postSet.AddAsync(newPost);

        }

        public async Task EditPost(PostUpdateDto updateDto)
        {
            Post? toUpdate = await _postSet.FirstOrDefaultAsync(p => p.Id == updateDto.Id);
            if (toUpdate == null)
            {
                throw new BadRequestExtention(ErrorMessages.PostNotFound);
            }
            toUpdate.Content = updateDto.Content;
            toUpdate.EditTime = DateTime.Now;
            _postSet.Update(toUpdate);
        }

        public async Task<IEnumerable<PostDto>> GetPostsForEvent(int EventID)
        {
            List<PostDto> posts = new();
            posts = await _postSet.Where(p => p.EventId == EventID && p.Deleted == false).Include(e => e.Event).Include(p => p.Creator).Select(p => _mapper.Map<PostDto>(p)).ToListAsync();
            return posts;

        }

        public async Task<IEnumerable<PostDto>> GetPostsForMember(int UserId)
        {
            List<PostDto> posts = new();
            posts = await _postSet.Where(p => p.CreatorId == UserId && p.Deleted == false).Include(e => e.Event).Include(p => p.Creator).Select(p => _mapper.Map<PostDto>(p)).ToListAsync();
            return posts;
        }

        public async Task DeletePost(int postId)
        {
            Post? toDelete = await _postSet.FirstOrDefaultAsync(p => p.Id == postId);
            if (toDelete == null)
            {
                throw new BadRequestExtention(ErrorMessages.PostNotFound);
            }
            toDelete.Deleted = true;
            _postSet.Update(toDelete);
        }

        public async Task<List<EventMemberDto>> GetMembersForEvent(int eventId)
        {
            return await _userEvents.Where(e => e.EventId == eventId).Include(u => u.User).ThenInclude(c => c!.Photo).Select(u => _mapper.Map<EventMemberDto>(u)).ToListAsync();
        }

        public async Task RegisterToEvent(int eventId, int userId)
        {
            if (await _userEvents.AnyAsync(e => e.EventId == eventId && e.UserId == userId))
            {
                throw new BadRequestExtention(ErrorMessages.AlreadyRegistered);
            }
            await _userEvents.AddAsync(new UserEvent() { EventId = eventId, UserId = userId });
        }

        public async Task<bool> ChangeAttending(int eventId, int userId)
        {
            UserEvent? userEvent = await _userEvents.FirstOrDefaultAsync(e => e.EventId == eventId && e.UserId == userId);
            if (userEvent == null)
            {
                throw new BadRequestExtention(ErrorMessages.NotRegistered);
            }
            userEvent.Arriving = !userEvent.Arriving;
            _userEvents.Update(userEvent);
            return userEvent.Arriving;


        }

        public async Task<PhotoDto> AddPhoto(int eventId, IFormFile file)
        {
            Event? CurrentEvent = await base._dbSet.Include(p => p.Photos).FirstOrDefaultAsync(e => e.Id == eventId);
            if (CurrentEvent == null)
            {
                throw new BadRequestExtention(ErrorMessages.EventNotFound);
            }
            ImageUploadResult result = await _photoService.UploadPhotoAsync(file);
            if (result.Error != null)
            {
                throw new BadRequestExtention(ErrorMessages.PhotoUploadError);
            };
            Photo<Event> photo = new Photo<Event>()
            {
                PhotoUrl = result.SecureUrl.AbsoluteUri.ToString(),
                PublicId = result.PublicId,
                Descrption = file.Name,
            };
            if (CurrentEvent!.Photos!.Count == 0)
            {
                photo.IsMain = true;
            }
            CurrentEvent.Photos.Add(photo);
            _dbSet.Update(CurrentEvent);
            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task SetMainPhoto(int eventId, int photoId)
        {
            Event? CurrentEvent = await base._dbSet.Include(p => p.Photos).FirstOrDefaultAsync(e => e.Id == eventId);
            if (CurrentEvent == null)
            {
                throw new BadRequestExtention(ErrorMessages.EventNotFound);
            }
            Photo<Event>? photo = CurrentEvent!.Photos!.FirstOrDefault(p => p.Id == photoId);
            if (photo == null)
            {
                throw new BadRequestExtention(ErrorMessages.PhotoUploadError);
            }
            if (photo.IsMain)
            {
                return;
            }
            Photo<Event>? currentMainPhoto = CurrentEvent!.Photos!.FirstOrDefault(p => p.IsMain);
            currentMainPhoto!.IsMain = false;
            photo.IsMain = true;
            _dbSet.Update(CurrentEvent);
        }

        public async Task DeletePhoto(int eventId, int photoId)
        {
            Event? CurrentEvent = await base._dbSet.Include(p => p.Photos).FirstOrDefaultAsync(e => e.Id == eventId);
            if (CurrentEvent == null)
            {
                throw new BadRequestExtention(ErrorMessages.EventNotFound);
            }
            Photo<Event>? photo = CurrentEvent!.Photos!.FirstOrDefault(p => p.Id == photoId);
            if (photo == null)
            {
                throw new BadRequestExtention(ErrorMessages.PhotoNotFound);
            }
            if (photo.IsMain)
            {
                throw new BadRequestExtention(ErrorMessages.PhotoIsMain);
            }
            if (photo.PublicId != null)
            {
                DeletionResult result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Result == "ok")
                {
                    CurrentEvent!.Photos!.Remove(photo);
                    base.Update(CurrentEvent);
                   
                }
                else throw new BadRequestExtention(ErrorMessages.PhotoDeletionFailed);
            }
        }
    }
}
