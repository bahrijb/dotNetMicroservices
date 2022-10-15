using System;
using System.Collections.Generic;
using System.Text;

namespace OrderMe.Catalog.BusinessLogic.Category.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
