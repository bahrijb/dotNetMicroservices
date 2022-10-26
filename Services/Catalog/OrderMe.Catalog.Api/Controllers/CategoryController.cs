using Microsoft.AspNetCore.Mvc;
using OrderMe.Catalog.BusinessLogic.Category.Dtos;
using OrderMe.Catalog.BusinessLogic.Category.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<CategoryDto> Create(CategoryDto categoryDto)
        {
            return await _categoryService.Create(categoryDto);
        }
        [HttpGet]
        public async Task<List<CategoryDto>> GetAll()
        {
            return await _categoryService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<CategoryDto> GetById(int id)
        {
            return await _categoryService.GetById(id);
        }
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _categoryService.Delete(id);
        }
        [HttpPut("{id}")]
        public async Task<bool> Update(int id, CategoryDto categoryDto)
        {
            return await _categoryService.Update(id, categoryDto);
        }
    }
}
