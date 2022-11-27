using AutoMapper;
using OrderMe.Catalog.BusinessLogic.Item.Dtos;
using OrderMe.Integration.Models;
using System;

namespace OrderMe.Catalog.BusinessLogic.Item.Mappings
{
    public class ItemMapping : Profile
    {
        public ItemMapping()
        {
            CreateMap<DataAccess.Models.Item, ItemDto>().ConvertUsing(MapItemModelToDto);
            CreateMap<DataAccess.Models.Item, ItemMessageRequestDto>().ConvertUsing(MapItemModelToMessageDto);
            CreateMap<ItemDto, DataAccess.Models.Item>().ConvertUsing(MapItemDtoToModel);
        }

        private ItemMessageRequestDto MapItemModelToMessageDto(DataAccess.Models.Item src)
        {
            if (src == null)
                return null;

            return new ItemMessageRequestDto()
            {
                ItemId = src.ItemId,
                Amount = src.Amount,
                Price = src.Price,
                Image = src.Image,
                Name = src.Name
            };
        }

        private ItemDto MapItemModelToDto(DataAccess.Models.Item src)
        {
            if (src == null)
                return null;

            return new ItemDto()
            {
                ItemId = src.ItemId,
                Amount = src.Amount,
                Price = src.Price,
                Description = src.Description,
                CategoryId = src.CategoryId,
                Image = src.Image,
                Name = src.Name
            };
        }

        private DataAccess.Models.Item MapItemDtoToModel(ItemDto src)
        {
            if (src == null)
                return null;

            return new DataAccess.Models.Item()
            {
                ItemId = src.ItemId.HasValue ? src.ItemId.Value : 0,
                Amount = src.Amount,
                Price = src.Price,
                Description = src.Description,
                CategoryId = src.CategoryId,
                Image = src.Image,
                Name = src.Name
            };
        }
    }
}
