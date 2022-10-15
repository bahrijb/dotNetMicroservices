using OrderMe.Catalog.BusinessLogic.Category.Dtos;
using OrderMe.Catalog.DataAccess.Contexts;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OrderMe.Catalog.BusinessLogic.Category.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<bool> Delete(int id)
        {
            var category = await _context.Categories.Where(a => a.CategoryId == id).FirstOrDefaultAsync();
            if (category == null) return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return false;
        }

        public async Task<bool> Update(int id, CategoryDto categoryDto)
        {
            var category = _context.Categories.Where(a => a.CategoryId == id).FirstOrDefault();
            if (category == null) return false;
            else
            {
                category = _mapper.Map<DataAccess.Models.Category>(categoryDto);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
