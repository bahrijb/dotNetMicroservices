using Microsoft.AspNetCore.Mvc;
using OrderMe.Catalog.BusinessLogic.Item.Dtos;
using OrderMe.Catalog.BusinessLogic.Item.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _ItemService;
        public ItemController(IItemService ItemService)
        {
            _ItemService = ItemService;
        }

        [HttpPost]
        public async Task<ItemDto> Create(ItemDto ItemDto)
        {
            return await _ItemService.Create(ItemDto);
        }
        [HttpGet]
        public async Task<List<ItemDto>> GetAll()
        {
            return await _ItemService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ItemDto> GetById(int id)
        {
            return await _ItemService.GetById(id);
        }
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _ItemService.Delete(id);
        }
        [HttpPut("{id}")]
        public async Task<bool> Update(int id, ItemDto ItemDto)
        {
            return await _ItemService.Update(id, ItemDto);
        }
    }
}
