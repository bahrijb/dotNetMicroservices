using MongoDB.Bson.Serialization.Attributes;

namespace OrderMe.Cart.DataAccess.Models
{
    public class CartItem
    {
        [BsonElement("Id")]
        public int ItemId { get; set; }
        [BsonElement("Name")]
        public string ItemName { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
