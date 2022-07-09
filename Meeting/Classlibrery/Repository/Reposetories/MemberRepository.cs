using AutoMapper;
using CloudinaryDotNet.Actions;
using DataAccess.Data.Entities;
using DataAccess.DTOs;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.ErrorHandling;
using DataAccess.Repository.IRepository;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Reposetories
{
    
    public class MemberRepository : BaseRepository<User, MemberDto>, IMemberRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly DbSet<Photo<User>> _photos;

        public MemberRepository(DbContext dbContext, UserManager<User> userManager, IPhotoService photoService,IMapper mapper) : base(dbContext)
        {
            this._userManager = userManager;
            this._photoService = photoService;
            this._mapper = mapper;
            this._photos = dbContext.Set<Photo<User>>();
        }

        public async Task<MemberDto> GetMember(int id)
        {
            User? user = await _userManager.Users.Include(p => p.Photo).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new BadRequestExtention(ErrorMessages.UserNotFound);
            }
            return _mapper.Map<MemberDto>(user);
        }
        public async Task UpdateLocation(MemberUpdateDto updateDto, int userId)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new BadRequestExtention(ErrorMessages.UserNotFound);
            }
            user.City = updateDto.City;
            user.Country = updateDto.Country;
            Update(user);
        }
        public async Task<PhotoDto> AddPhoto(IFormFile file, int userId)
        {
            User? user = await _userManager.Users.Include(p => p.Photo).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new BadRequestExtention(ErrorMessages.UserNotFound);
            }
            ImageUploadResult result = await _photoService.UploadPhotoAsync(file);
            if (result.Error != null)
            {
                throw new BadRequestExtention(ErrorMessages.PhotoUploadFailed);
            };
            await DeletePhoto(user.Photo!.PhotoUrl!);
            Photo<User> photo = new Photo<User>()
            {
                PhotoUrl = result.SecureUrl.AbsoluteUri.ToString(),
                PublicId = result.PublicId,
                Uploader = user
            };
            user.Photo = photo;
            Update(user);
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
