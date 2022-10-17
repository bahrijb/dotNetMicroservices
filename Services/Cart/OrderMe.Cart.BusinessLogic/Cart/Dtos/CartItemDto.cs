namespace OrderMe.Cart.BusinessLogic.Cart.Dtos
{
    public class CartItemDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
