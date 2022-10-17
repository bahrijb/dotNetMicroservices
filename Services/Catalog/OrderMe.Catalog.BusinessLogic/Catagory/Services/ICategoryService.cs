using OrderMe.Catalog.BusinessLogic.Category.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Catalog.BusinessLogic.Category.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> Create(CategoryDto categoryDto);
        Task<bool> Delete(int id);
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(int categoryId);
        Task<bool> Update(int id, CategoryDto categoryDto);
    }
}