using System;

namespace OrderMe.Integration.Models
{
    public class ItemMessageRequestDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}
