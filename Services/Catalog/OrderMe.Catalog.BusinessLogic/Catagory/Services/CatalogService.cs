using OrderMe.Catalog.BusinessLogic.Category.Dtos;
using OrderMe.Catalog.DataAccess.Contexts;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using OrderMe.Catalog.BusinessLogic.Item.Services;
using OrderMe.Catalog.BusinessLogic.Item.Dtos;

namespace OrderMe.Catalog.BusinessLogic.Category.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;

        public CategoryService(ICatalogDbContext context, IMapper mapper, IItemService itemService)
        {
            _context = context;
            _mapper = mapper;
            _itemService = itemService;
        }

        public async Task<CategoryDto> Create(CategoryDto categoryDto)
        {
            var categoryToAdd = _mapper.Map<DataAccess.Models.Category>(categoryDto);
            _context.Categories.Add(categoryToAdd);
            await _context.SaveChangesAsync();
            return categoryDto;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            if (categories == null || !categories.Any()) return new List<CategoryDto>();
            var existingCategories = _mapper.Map<List<DataAccess.Models.Category>, List<CategoryDto>>(categories);
            return existingCategories;
        }

        public async Task<CategoryDto> GetById(int categoryId)
        {
            var category = await _context.Categories.Where(a => a.CategoryId == categoryId).FirstOrDefaultAsync();
            if (category == null) return new CategoryDto();
            var existingCategory = _mapper.Map<CategoryDto>(category);
            return existingCategory;
        }

        public async Task<bool> Delete(int categoryId)
        {
            var category = await _context.Categories.Where(a => a.CategoryId == categoryId).FirstOrDefaultAsync();
            if (category == null) return false;
            await _itemService.DeleteByCategoryId(categoryId);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return false;
        }

        public async Task<bool> Update(int categoryId, CategoryDto categoryDto)
        {
            var categoryExists = await _context.Categories.Where(a => a.CategoryId == categoryId).AnyAsync();
            if (categoryExists)
            {
                var category = _mapper.Map<DataAccess.Models.Category>(categoryDto);
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ItemDto>> AddItemToCategory(int categoryId, ItemDto item)
        {
            var categoryExists = await _context.Categories.Where(a => a.CategoryId == categoryId).AnyAsync();
            if (categoryExists)
            {
                await _itemService.Create(item);
                await _context.SaveChangesAsync();
            }
            return await _itemService.GetByCategoryId(categoryId);
        }

        public async Task<List<ItemDto>> UpdateItemInCategory(int categoryId, ItemDto item)
        {
            var categoryExists = await _context.Categories.Where(a => a.CategoryId == categoryId).AnyAsync();
            if (categoryExists)
            {
                await _itemService.Update(item.ItemId.Value, item);
                await _context.SaveChangesAsync();
            }
            return await _itemService.GetByCategoryId(categoryId);
        }

        public async Task<List<ItemDto>> DeleteItemFromCategory(int categoryId, int itemId)
        {
            var categoryExists = await _context.Categories.Where(a => a.CategoryId == categoryId).AnyAsync();
            if (categoryExists)
            {
                await _itemService.Delete(itemId);
                await _context.SaveChangesAsync();
            }
            return await _itemService.GetByCategoryId(categoryId);
        }

        public async Task<List<ItemDto>> GetAllItemsByCategory(int categoryId)
        {
            return await _itemService.GetByCategoryId(categoryId);
        }
    }
}
