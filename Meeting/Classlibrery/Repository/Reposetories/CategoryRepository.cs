using AutoMapper;
using DataAccess.Data.Entities;
using DataAccess.Repository.IRepository;
using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.ErrorHandling;
using DataAccess.DTOs.UpdateDtos;

namespace DataAccess.Repository.Reposetories
{
    public class CategoryRepository : BaseRepository<Category, CategoryDto>, ICategoryRepository
    {

        private readonly IMapper _mapper;
        public CategoryRepository(DbContext dbContext, IMapper mapper) : base(dbContext)
        {
            {
                _mapper = mapper;
            }
        }

        public async Task Add(CategoryCreationDto entity)
        {
            if (await base._dbSet.AnyAsync(c => c.CategoryName == entity.CategoryName))
                throw new BadRequestExtention(ErrorMessages.CategoryExist);
            Category category = _mapper.Map(entity, new Category());
            base._dbSet.Add(category);
        }

        public async Task EditCategory(CategoryUpdateDto category)
        {
            Category? categoryEntity = await base._dbSet.FindAsync(category.Id);
            if (categoryEntity == null)
                throw new BadRequestExtention(ErrorMessages.CategoryNotFound);
            categoryEntity.CategoryName = category.CategoryName;
            categoryEntity.Description = category.Description;
            base.Update(categoryEntity);
        }


        public new async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return _mapper.Map(await base._dbSet.ToListAsync(), new List<CategoryDto>());
        }

        public new async Task<CategoryDto> GetById(int id)
        {
            return _mapper.Map(await base._dbSet.FindAsync(id), new CategoryDto());
        }



    }
}
