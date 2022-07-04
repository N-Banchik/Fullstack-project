using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IPostsRepository
    {
        Task CreatePost(PostCreationDto creationDto,int UserId);
        Task EditPost(PostUpdateDto updateDto);
        Task DeletePost(int postId);
        Task<IEnumerable<PostDto>> GetPostsForEvent(int EventID);
        Task<IEnumerable<PostDto>> GetPostsForMember(int UserId);
    }
}
