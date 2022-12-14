using Microsoft.AspNetCore.Mvc;
using OrderMe.Catalog.BusinessLogic.Category.Dtos;
using OrderMe.Catalog.BusinessLogic.Category.Services;
using OrderMe.Catalog.BusinessLogic.Item.Dtos;
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


        [HttpGet("items/{id}")]
        public async Task<List<ItemDto>> GetAllItemsByCategory(int id)
        {
            return await _categoryService.GetAllItemsByCategory(id);
        }

        [HttpPost("items/{id}")]
        public async Task<List<ItemDto>> AddItemToCategory(int id, ItemDto item)
        {
            return await _categoryService.AddItemToCategory(id, item);
        }

        [HttpDelete("items/{id}")]
        public async Task<List<ItemDto>> DeleteItemFromCategory(int id, int itemId)
        {
            return await _categoryService.DeleteItemFromCategory(id, itemId);
        }

        [HttpPut("items/{id}")]
        public async Task<List<ItemDto>> UpdateItemInCategory(int id, ItemDto item)
        {
            return await _categoryService.UpdateItemInCategory(id, item);
        }
    }
}
