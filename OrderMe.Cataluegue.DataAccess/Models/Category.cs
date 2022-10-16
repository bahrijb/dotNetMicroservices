using System;
using System.Collections.Generic;
using System.Text;

namespace OrderMe.Catalog.DataAccess.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public virtual List<Category> ChildCategories { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}
