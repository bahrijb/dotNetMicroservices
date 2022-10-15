using AutoMapper;
using OrderMe.Catalog.BusinessLogic.Category.Dtos;

namespace OrderMe.Catalog.BusinessLogic.Category.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<DataAccess.Models.Category, CategoryDto>().ConvertUsing(MapCategoryModelToDto);
            CreateMap<CategoryDto, DataAccess.Models.Category>().ConvertUsing(MapCategoryDtoToModel);
        }

        private CategoryDto MapCategoryModelToDto(DataAccess.Models.Category src)
        {
            if (src == null)
                return null;

            return new CategoryDto()
            {
                CategoryId = src.CategoryId,
                Image = src.Image,
                Name = src.Name,
                ParentCategoryId = src.ParentCategoryId
            };
        }

        private DataAccess.Models.Category MapCategoryDtoToModel(CategoryDto src)
        {
            if (src == null)
                return null;

            return new DataAccess.Models.Category()
            {
                CategoryId = src.CategoryId,
                Image = src.Image,
                Name = src.Name,
                ParentCategoryId = src.ParentCategoryId
            };
        }
    }
}
