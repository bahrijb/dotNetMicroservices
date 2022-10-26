using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using AutoMapper;
using System.Linq;
using OrderMe.Cart.DataAccess.Repositories;

namespace OrderMe.Cart.BusinessLogic.Cart.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        public CartService(IMapper mapper, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<List<Dtos.CartItemDto>> GetItemsByCartId(string cartId)
        {
            var cart = await _cartRepository.GetById(cartId);
            var cartDto = _mapper.Map<Dtos.CartDto>(cart);
            return cartDto.Items;
        }

        public async Task<Dtos.CartDto> AddItemToCart(string cartId, Dtos.CartItemDto cartItem)
        {
            var isCartExists = await _cartRepository.IsCartExists(cartId);
            if (isCartExists)
            {
                var existingCart = await _cartRepository.GetById(cartId);
                var existingCartDto = _mapper.Map<Dtos.CartDto>(existingCart);
                existingCartDto.Items.Add(cartItem);

                var existingCartToUpdate = _mapper.Map<DataAccess.Models.Cart>(existingCartDto);
                await _cartRepository.Update(existingCartToUpdate);
                
                var upatedCart = await _cartRepository.GetById(cartId);
                return _mapper.Map<Dtos.CartDto>(upatedCart);
            }

            var createCartDto = new Dtos.CartDto
            {
                CartId = cartId,
                Items = new List<Dtos.CartItemDto>() { cartItem }
            };
            var cartToCreate = _mapper.Map<DataAccess.Models.Cart>(createCartDto);
            var isCreated = await _cartRepository.Create(cartToCreate);

            if (isCreated)
            {
                return createCartDto;
            }

            else return new Dtos.CartDto();
        }

        public async Task<Dtos.CartDto> RemoveItemFromCart(string cartId, int itemId)
        {
            var isCartExists = await _cartRepository.IsCartExists(cartId);
            if (isCartExists)
            {
                var existingCart = await _cartRepository.GetById(cartId);
                var existingCartDto = _mapper.Map<Dtos.CartDto>(existingCart);
                var itemToRemove = existingCartDto.Items.Where(x => x.ItemId == itemId).FirstOrDefault();
                if(itemToRemove != null)
                {
                    existingCartDto.Items.Remove(itemToRemove);

                    var existingCartToUpdate = _mapper.Map<DataAccess.Models.Cart>(existingCartDto);
                    await _cartRepository.Update(existingCartToUpdate);

                    var upatedCart = await _cartRepository.GetById(cartId);
                    return _mapper.Map<Dtos.CartDto>(upatedCart);
                }
                return existingCartDto;
            }
            return new Dtos.CartDto();
        }

        public async Task<bool> DeleteById(string cartId)
        {
            return await _cartRepository.DeleteById(cartId);
        }

        public async Task<List<Dtos.CartDto>> GetAllCarts()
        {
            var carts = await _cartRepository.GetAllCarts();
            var existingCarts = _mapper.Map<List<DataAccess.Models.Cart>, List<Dtos.CartDto>>(carts);
            return existingCarts;
        }
    }
}
