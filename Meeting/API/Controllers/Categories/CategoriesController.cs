using DataAccess.Data.Entities;
using DataAccess.DTOs;
using DataAccess.DTOs.Creation_Dtos;
using DataAccess.DTOs.UpdateDtos;
using DataAccess.ErrorHandling;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Categories
{
    [Authorize]
    public class CategoriesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _unitOfWork._categoryRepository.GetAll());
            }
            catch (BadRequestExtention ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedExtention ex)
            {
                return Unauthorized(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int Id)
        {
            try
            {
                return Ok(await _unitOfWork._categoryRepository.GetById(Id));
            }
            catch (BadRequestExtention ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedExtention ex)
            {
                return Unauthorized(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryCreationDto creationDto)
        {
           
            try
            {
                await _unitOfWork._categoryRepository.Add(creationDto);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (BadRequestExtention ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedExtention ex)
            {
                return Unauthorized(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(CategoryUpdateDto updateDto)
        {
            

            try
            {
                await _unitOfWork._categoryRepository.EditCategory(updateDto);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (BadRequestExtention ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedExtention ex)
            {
                return Unauthorized(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }




    }


}
