using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderMe.Cart.BusinessLogic.Cart.Services;
using OrderMe.Cart.BusinessLogic.Cart.Mappings;
using OrderMe.Cart.BusinessLogic.Cart.Dtos;
using Moq;
using AutoMapper;
using OrderMe.Cart.DataAccess.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Cart.BusinessLogic.Test
{
    [TestClass]
    public class CartServiceTests
    {
        private Mock<ICartRepository> _mockCartRepo = new Mock<ICartRepository>();
        private IMapper _mapper;
        private ICartService _cartService;

        public CartServiceTests()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CartMapping());
            });
            _mapper = mapperConfig.CreateMapper();
            _cartService = new CartService(_mapper, _mockCartRepo.Object);
        }

        [TestMethod]
        public async Task GetItemsByCartId_Valid()
        {
            //Arrange
            var cart = CreateCartTestData();
            var responseCart = new CartDto()
            {
                CartId = cart.CartId,
                Items = new List<CartItemDto>() { cart.Items[0] }
            };
            _mockCartRepo.Setup(x => x.Create(It.IsAny<DataAccess.Models.Cart>())).ReturnsAsync(value: true);

            //Act
            var response = await _cartService.AddItemToCart(cart.CartId, cart.Items[0]);

            //Assert
            Assert.AreEqual(responseCart.Items[0], response.Items[0]);
        }

        private CartDto CreateCartTestData()
        {
            return new CartDto()
            {
                CartId = "cart01",
                Items = CreateItemsTestData()
            };
        }

        private List<CartItemDto> CreateItemsTestData()
        {
            return new List<CartItemDto>()
            {
                new CartItemDto()
                {
                    Image = "image",
                    ItemId = 1,
                    ItemName = "name",
                    Price = 12,
                    Quantity = 10
                },
                new CartItemDto()
                {
                    Image = "image",
                    ItemId = 2,
                    ItemName = "name",
                    Price = 12,
                    Quantity = 10
                }
            };
        }
    }
}
