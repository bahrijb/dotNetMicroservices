using System;
using System.Collections.Generic;
using System.Text;

namespace OrderMe.Cart.DataAccess.Models
{
    public class Item
    {
        [BsonElement("Name")]
        public string ItemName { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
