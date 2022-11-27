using OrderMe.Cart.BusinessLogic.Cart.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Cart.BusinessLogic.Cart.Services
{
    public interface ICartService
    {
        Task<List<CartDto>> GetAllCarts();
        Task<bool> DeleteById(string cartId);
        Task<CartDto> GetCartById(string cartId);
        Task<List<CartItemDto>> GetItemsByCartId(string cartId);
        Task<CartDto> AddItemToCart(string cartId, CartItemDto cartItem);
        Task<CartDto> RemoveItemFromCart(string cartId, int itemId);
        Task<bool> UpdateItemInAllCarts(CartItemDto cartItem);
    }
}