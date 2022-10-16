﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OrderMe.Catalog.DataAccess.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public Category Category { get; set; }
    }
}
