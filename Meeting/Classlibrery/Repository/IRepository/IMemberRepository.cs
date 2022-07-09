using DataAccess.DTOs;
using DataAccess.DTOs.UpdateDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IMemberRepository
    {

        Task UpdateLocation(MemberUpdateDto updateDto, int userId);
        Task<PhotoDto> AddPhoto(IFormFile file, int userId);
        Task<MemberDto> GetMember(int userId);


    }
}
