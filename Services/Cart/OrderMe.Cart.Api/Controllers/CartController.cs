using Microsoft.AspNetCore.Mvc;
using OrderMe.Cart.BusinessLogic.Cart.Dtos;
using OrderMe.Cart.BusinessLogic.Cart.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Cart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        //[HttpGet("{cartId}", Name = "GetItemsByCartId")]
        //public async Task<List<CartItemDto>> GetItemsByCartId(string cartId)
        //{
        //    return await _cartService.GetItemsByCartId(cartId);
        //}

        [HttpGet("{cartId}", Name = "GetCartById")]
        public async Task<CartDto> GetCartById(string cartId)
        {
            return await _cartService.GetCartById(cartId);
        }

        [HttpPost("{cartId}", Name = "AddItemToCart")]
        public async Task<CartDto> AddItemToCart(string cartId, CartItemDto cartItem)
        {
            return await _cartService.AddItemToCart(cartId, cartItem);
        }

        [HttpPut("{cartId},{itemId}", Name = "RemoveItemFromCart")]
        public async Task<CartDto> RemoveItemFromCart(string cartId, int itemId)
        {
            return await _cartService.RemoveItemFromCart(cartId, itemId);
        }

        //[HttpDelete("{cartId}", Name = "DeleteCart")]
        //public async Task<bool> DeleteCart(string cartId)
        //{
        //    return await _cartService.DeleteById(cartId);
        //}

        //[HttpGet(Name = "GetAllCarts")]
        //public async Task<List<CartDto>> GetAllCarts()
        //{
        //    return await _cartService.GetAllCarts();
        //}
    }
}
