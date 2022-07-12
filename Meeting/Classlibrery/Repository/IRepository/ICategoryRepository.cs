using DataAccess.Data.Entities;
using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;

namespace DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
         Task Add(CategoryCreationDto entity);
        Task EditCategory(CategoryUpdateDto category);
        new Task<IEnumerable<CategoryDto>> GetAll();
        new Task<CategoryDto> GetById(int id);
        
    }

}
