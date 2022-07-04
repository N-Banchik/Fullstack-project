using AutoMapper;
using DataAccess.Data.Entities;
using DataAccess.Data.Entities.Bridge_Entities;
using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.DTOs.ViewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<Hobby, HobbyDto>();
            CreateMap<HobbyCreationDto, Hobby>();
            CreateMap<HobbyUpdateDto, Hobby>();
            CreateMap<Hobby, HobbyViewDto>()
                .ForMember(dest => dest.MainPhotoUrl, src => src.MapFrom(opt => opt.Photo!.PhotoUrl));

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreationDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Guide, GuideDto>();
            CreateMap<Guide, GuideViewDto>();
            CreateMap<GuideCreationDto, Guide>();
            CreateMap<GuideUpdateDto, Guide>();

            CreateMap<User, UserDto>();
            CreateMap<User, MemberDto>();
            CreateMap<UserEvent, EventMemberDto>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.User!.Photo!.PhotoUrl))
                .ForMember(u => u.UserName, u => u.MapFrom(u => u.User!.UserName));



            CreateMap<Event, EventDto>().ForMember(destination => destination.MainPhotoUrl,
             opt => { opt.MapFrom(src => src.Photos!.FirstOrDefault(p => p.IsMain)!.PhotoUrl); });
            CreateMap<Event, EventViewDto>().ForMember(destination => destination.MainPhotoUrl,
             opt => { opt.MapFrom(src => src.Photos!.FirstOrDefault(p => p.IsMain)!.PhotoUrl); });
            CreateMap<EventCreationDto, Event>();
            CreateMap<EventUpdateDto, Event>();
            CreateMap<Event, EventViewDto>().ForMember(destination => destination.MainPhotoUrl,
             opt => { opt.MapFrom(src => src.Photos!.FirstOrDefault(p => p.IsMain)!.PhotoUrl); });

            CreateMap<Post, PostDto>().ForMember(p => p.CreatorUserName, x => x.MapFrom(u => u.Creator!.UserName))
                .ForMember(p => p.CreatorPhotoUrl, x => x.MapFrom(u => u.Creator!.Photo!.PhotoUrl))
                .ForMember(p => p.EventName, x => x.MapFrom(e => e.Event!.EventTitle));

            CreateMap<PostCreationDto, Post>();
            CreateMap<Photo<User>, PhotoDto>().ForMember(p => p.UploaderUserName,
    x => x.MapFrom(u => u.Uploader!.UserName));
            CreateMap<Photo<Hobby>, PhotoDto>().ForMember(p => p.UploaderUserName,
                x => x.MapFrom(u => u.Uploader!.UserName));
            CreateMap<Photo<Event>, PhotoDto>().ForMember(p => p.UploaderUserName,
                x => x.MapFrom(u => u.Uploader!.UserName));

        }
    }
}
