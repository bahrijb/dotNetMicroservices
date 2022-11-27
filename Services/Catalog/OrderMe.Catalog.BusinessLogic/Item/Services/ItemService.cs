using OrderMe.Catalog.BusinessLogic.Item.Dtos;
using OrderMe.Catalog.DataAccess.Contexts;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using MassTransit;
using OrderMe.Integration.Models;

namespace OrderMe.Catalog.BusinessLogic.Item.Services
{
    public class ItemService : IItemService
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public ItemService(ICatalogDbContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<ItemDto> Create(ItemDto ItemDto)
        {
            var itemExist = await _context.Items.Where(a => a.ItemId == ItemDto.ItemId).AnyAsync();
            var ItemToAdd = _mapper.Map<DataAccess.Models.Item>(ItemDto);
            if (!itemExist)
            {
                _context.Items.Add(ItemToAdd);
                await _context.SaveChangesAsync();
            }
            return ItemDto;
        }

        public async Task<List<ItemDto>> GetAll()
        {
            var items = await _context.Items.ToListAsync();
            if (items == null || !items.Any()) return new List<ItemDto>();
            var existingItems = _mapper.Map<List<DataAccess.Models.Item>, List<ItemDto>>(items);
            return existingItems;
        }

        public async Task<ItemDto> GetById(int ItemId)
        {
            var item = await _context.Items.Where(a => a.ItemId == ItemId).FirstOrDefaultAsync();
            if (item == null) return new ItemDto();
            var existingItem = _mapper.Map<ItemDto>(item);
            return existingItem;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.Items.Where(a => a.ItemId == id).FirstOrDefaultAsync();
            if (item == null) return false;
            _context.Items.Remove(item);
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

                Uri uri = new Uri("rabbitmq://localhost/itemQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                var itemUpdateMessage = _mapper.Map<ItemMessageRequestDto>(item);
                await endPoint.Send(itemUpdateMessage);

                return true;
            }
            return false;
        }

        public async Task<List<ItemDto>> GetByCategoryId(int CategoryId)
        {
            var items = await _context.Items.Where(x => x.CategoryId == CategoryId).ToListAsync();
            if (items == null || !items.Any()) return new List<ItemDto>();
            var existingItems = _mapper.Map<List<DataAccess.Models.Item>, List<ItemDto>>(items);
            return existingItems;
        }

        public async Task<bool> DeleteByCategoryId(int CategoryId)
        {
            var items = await _context.Items.Where(x => x.CategoryId == CategoryId).ToListAsync();
            if (items == null || !items.Any()) return true;
            _context.Items.RemoveRange(items);
            return true;
        }
    }
}
