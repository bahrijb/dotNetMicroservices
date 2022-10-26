using OrderMe.Catalog.BusinessLogic.Item.Dtos;
using OrderMe.Catalog.DataAccess.Contexts;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OrderMe.Catalog.BusinessLogic.Item.Services
{
    public class ItemService : IItemService
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public ItemService(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemDto> Create(ItemDto ItemDto)
        {
            var ItemToAdd = _mapper.Map<DataAccess.Models.Item>(ItemDto);
            _context.Items.Add(ItemToAdd);
            await _context.SaveChangesAsync();
            return ItemDto;
        }

        public async Task<List<ItemDto>> GetAll()
        {
            var Items = await _context.Items.ToListAsync();
            if (Items == null || !Items.Any()) return new List<ItemDto>();
            var existingItems = _mapper.Map<List<DataAccess.Models.Item>, List<ItemDto>>(Items);
            return existingItems;
        }

        public async Task<ItemDto> GetById(int ItemId)
        {
            var Item = await _context.Items.Where(a => a.ItemId == ItemId).FirstOrDefaultAsync();
            if (Item == null) return new ItemDto();
            var existingItem = _mapper.Map<ItemDto>(Item);
            return existingItem;
        }

        public async Task<bool> Delete(int id)
        {
            var Item = await _context.Items.Where(a => a.ItemId == id).FirstOrDefaultAsync();
            if (Item == null) return false;
            _context.Items.Remove(Item);
            await _context.SaveChangesAsync();
            return false;
        }

        public async Task<bool> Update(int id, ItemDto ItemDto)
        {
            var itemExist = await _context.Items.Where(a => a.ItemId == id).AnyAsync();
            if (itemExist) 
            {
                var item = _mapper.Map<DataAccess.Models.Item>(ItemDto);
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
