using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderMe.Cart.DataAccess.Models
{
    public class Cart
    {
        [BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.String)]
        public string CartId { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
