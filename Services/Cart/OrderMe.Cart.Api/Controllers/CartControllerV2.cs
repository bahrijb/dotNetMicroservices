using Microsoft.AspNetCore.Mvc;
using OrderMe.Cart.BusinessLogic.Cart.Dtos;
using OrderMe.Cart.BusinessLogic.Cart.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Cart.Api.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class CartControllerV2 : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartControllerV2(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{cartId}", Name = "GetItemsByCartId")]
        public async Task<List<CartItemDto>> GetItemsByCartId(string cartId)
        {
            return await _cartService.GetItemsByCartId(cartId);
        }
    }
}
