using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderMe.Catalog.BusinessLogic.Category.Dtos;
using OrderMe.Catalog.BusinessLogic.Category.Mappings;
using OrderMe.Catalog.BusinessLogic.Category.Services;
using OrderMe.Catalog.DataAccess.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMe.Catalog.BusinessLogic.Test
{
    [TestClass]
    public class CategoryServiceTest
    {
        private Mock<ICatalogDbContext> _mockDbContext = new Mock<ICatalogDbContext>();
        private IMapper _mapper;
        private ICategoryService _categoryService;

        public CategoryServiceTest()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CategoryMapping());
            });
            _mapper = mapperConfig.CreateMapper();
            _categoryService = new CategoryService(_mockDbContext.Object, _mapper);
        }

        [TestMethod]
        public async Task GetById_Valid()
        {
            //Arrange
            var expectedCategory = CreateCategoryTestData();

            //Act
            var response = await _categoryService.GetById(expectedCategory.CategoryId);
            //Assert
            Assert.AreEqual(expectedCategory, response);
        }

        private DataAccess.Models.Category CreateCategoryTestData()
        {
            return new DataAccess.Models.Category()
            {
                CategoryId = 01,
                Image = "image",
                Name = "cat01",
                ParentCategoryId = null
            };
        }
    }
}
