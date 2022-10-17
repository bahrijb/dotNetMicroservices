using AutoMapper;
using OrderMe.Cart.BusinessLogic.Cart.Dtos;

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

            return new CartDto()
            {
                CartId = src.CartId,
                Image = src.Image,
                Name = src.Name,
                ParentCartId = src.ParentCartId
            };
        }

        private DataAccess.Models.Cart MapCartDtoToModel(CartDto src)
        {
            if (src == null)
                return null;

            return new DataAccess.Models.Cart()
            {
                CartId = src.CartId.HasValue ? src.CartId.Value : 0,
                Image = src.Image,
                Name = src.Name,
                ParentCartId = src.ParentCartId
            };
        }
    }
}
