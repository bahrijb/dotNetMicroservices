using System;

namespace OrderMe.Integration.Shared.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}
