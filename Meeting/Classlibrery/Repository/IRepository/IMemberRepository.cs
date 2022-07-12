using DataAccess.DTOs;
using DataAccess.DTOs.UpdateDtos;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Repository.IRepository
{
    public interface IMemberRepository
    {

        Task UpdateLocation(MemberUpdateDto updateDto, int userId);
        Task<PhotoDto> AddPhoto(IFormFile file, int userId);
        Task<MemberDto> GetMember(int userId);


    }
}
