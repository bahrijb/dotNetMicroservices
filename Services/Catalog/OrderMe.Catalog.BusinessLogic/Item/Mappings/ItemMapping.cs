using AutoMapper;
using OrderMe.Catalog.BusinessLogic.Item.Dtos;

namespace OrderMe.Catalog.BusinessLogic.Item.Mappings
{
    public class ItemMapping : Profile
    {
        public ItemMapping()
        {
            CreateMap<DataAccess.Models.Item, ItemDto>().ConvertUsing(MapItemModelToDto);
            CreateMap<ItemDto, DataAccess.Models.Item>().ConvertUsing(MapItemDtoToModel);
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
