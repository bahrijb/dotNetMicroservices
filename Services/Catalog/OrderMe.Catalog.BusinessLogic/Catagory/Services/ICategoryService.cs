using OrderMe.Catalog.BusinessLogic.Category.Dtos;
using OrderMe.Catalog.BusinessLogic.Item.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Catalog.BusinessLogic.Category.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> Create(CategoryDto categoryDto);
        Task<bool> Delete(int categoryId);
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(int categoryId);
        Task<bool> Update(int categoryId, CategoryDto categoryDto);
        Task<List<ItemDto>> UpdateItemInCategory(int categoryId, ItemDto item);
        Task<List<ItemDto>> AddItemToCategory(int categoryId, ItemDto item);
        Task<List<ItemDto>> DeleteItemFromCategory(int categoryId, int itemId);
        Task<List<ItemDto>> GetAllItemsByCategory(int categoryId);
    }
}