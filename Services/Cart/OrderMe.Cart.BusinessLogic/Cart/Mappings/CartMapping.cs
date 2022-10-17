using AutoMapper;
using OrderMe.Cart.BusinessLogic.Cart.Dtos;
using System.Collections.Generic;

namespace OrderMe.Cart.BusinessLogic.Cart.Mappings
{
    public class CartMapping : Profile
    {
        public CartMapping()
        {
            CreateMap<DataAccess.Models.Cart, CartDto>().ConvertUsing(MapCartModelToDto);
            CreateMap<CartDto, DataAccess.Models.Cart>().ConvertUsing(MapCartDtoToModel);
        }

        private CartDto MapCartModelToDto(DataAccess.Models.Cart src)
        {
            if (src == null)
                return null;

            var itemDtos = new List<CartItemDto>();

            foreach(var item in src.Items)
            {
                itemDtos.Add(new CartItemDto
                {
                    ItemId = item.ItemId,
                    Image = item.Image,
                    ItemName = item.ItemName,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

            return new CartDto()
            {
                CartId = src.CartId,
                Items = itemDtos
            };
        }

        private DataAccess.Models.Cart MapCartDtoToModel(CartDto src)
        {
            if (src == null)
                return null;

            var items = new List<DataAccess.Models.CartItem>();
            foreach (var item in src.Items)
            {
                items.Add(new DataAccess.Models.CartItem
                {
                    ItemId = item.ItemId,
                    Image = item.Image,
                    ItemName = item.ItemName,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

            return new DataAccess.Models.Cart()
            {
                CartId = src.CartId,
                Items = items
            };
        }
    }
}
