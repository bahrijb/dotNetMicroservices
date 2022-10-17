using System.Collections.Generic;

namespace OrderMe.Cart.BusinessLogic.Cart.Dtos
{
    public class CartDto
    {
        public  string CartId { get; set; }
        public List<CartItemDto> Items { get; set; }
    }
}
