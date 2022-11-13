using OrderMe.Catalog.BusinessLogic.Item.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Catalog.BusinessLogic.Item.Services
{
    public interface IItemService
    {
        Task<ItemDto> Create(ItemDto ItemDto);
        Task<bool> Delete(int id);
        Task<List<ItemDto>> GetAll();
        Task<ItemDto> GetById(int ItemId);
        Task<List<ItemDto>> GetByCategoryId(int CategoryId);
        Task<bool> DeleteByCategoryId(int CategoryId);
        Task<bool> Update(int id, ItemDto ItemDto);
    }
}