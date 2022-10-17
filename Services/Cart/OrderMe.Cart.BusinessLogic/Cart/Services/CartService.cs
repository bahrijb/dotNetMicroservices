using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AutoMapper;
using OrderMe.Cart.DataAccess.Constants;
using System.Linq;

namespace OrderMe.Cart.BusinessLogic.Cart.Services
{
    public class CartService : ICartService
    {
        private readonly IMongoCollection<DataAccess.Models.Cart> _cartsCollection;
        private readonly IMapper _mapper;

        public CartService(IOptions<CartStoreDataBaseSetings> cartStoreDatabaseSettings, 
            IMapper mapper)
        {
            _mapper = mapper;
            var mongoDatabase = InitalizeCartConneciton(cartStoreDatabaseSettings);
            _cartsCollection = mongoDatabase.GetCollection<DataAccess.Models.Cart>(cartStoreDatabaseSettings.Value.CartCollectionName);
        }

        public async Task<List<Dtos.CartItemDto>> GetItemsByCartId(string cartId)
        {
            var cart = await GetById(cartId);
            return cart.Items;
        }

        public async Task<Dtos.CartDto> AddItemToCart(string cartId, Dtos.CartItemDto cartItem)
        {
            var isCartExists = await IsCartExists(cartId);
            if (isCartExists)
            {
                var existingCart = await GetById(cartId);
                existingCart.Items.Add(cartItem);
                await Update(existingCart);
                return await GetById(cartId);
            }
            return await Create(new Dtos.CartDto
            {
                CartId = cartId,
                Items = new List<Dtos.CartItemDto>() { cartItem }
            });
        }

        public async Task<Dtos.CartDto> RemoveItemFromCart(string cartId, int itemId)
        {
            var isCartExists = await IsCartExists(cartId);
            if (isCartExists)
            {
                var existingCart = await GetById(cartId);
                var itemToRemove = existingCart.Items.Where(x => x.ItemId == itemId).FirstOrDefault();
                if(itemToRemove != null)
                {
                    existingCart.Items.Remove(itemToRemove);
                    await Update(existingCart);
                    return await GetById(cartId);
                }
                return existingCart;
            }
            return new Dtos.CartDto();
        }

        public async Task<bool> DeleteById(string cartId)
        {
            var cart = await _cartsCollection.Find(x => x.CartId == cartId).FirstOrDefaultAsync();
            if (cart == null) return false;
            await _cartsCollection.DeleteOneAsync(x => x.CartId == cartId);
            return false;
        }

        public async Task<List<Dtos.CartDto>> GetAllCarts()
        {
            var carts = await _cartsCollection.Find(_ => true).ToListAsync();
            if (carts == null || !carts.Any()) return new List<Dtos.CartDto>();
            var existingCarts = _mapper.Map<List<DataAccess.Models.Cart>, List<Dtos.CartDto>>(carts);
            return existingCarts;
        }

        private IMongoDatabase InitalizeCartConneciton(IOptions<CartStoreDataBaseSetings> cartStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                cartStoreDatabaseSettings.Value.ConnectionString);

            return mongoClient.GetDatabase(
                cartStoreDatabaseSettings.Value.DatabaseName);
        }

        private async Task<Dtos.CartDto> Create(Dtos.CartDto cartDto)
        {
            var cartToAdd = _mapper.Map<DataAccess.Models.Cart>(cartDto);
            await _cartsCollection.InsertOneAsync(cartToAdd);
            return cartDto;
        }

        private async Task<Dtos.CartDto> GetById(string cartId)
        {
            var cart = await _cartsCollection.Find(x => x.CartId == cartId).FirstOrDefaultAsync();
            if (cart == null) return new Dtos.CartDto();
            var existingCart = _mapper.Map<Dtos.CartDto>(cart);
            return existingCart;
        }

        private async Task<bool> Update(Dtos.CartDto cartDto)
        {
            var cartExists = await _cartsCollection.Find(x => x.CartId == cartDto.CartId).AnyAsync();
            if (cartExists)
            {
                var cart = _mapper.Map<DataAccess.Models.Cart>(cartDto);
                await _cartsCollection.ReplaceOneAsync(x => x.CartId == cartDto.CartId, cart);
                return true;
            }
            return false;
        }

        private async Task<bool> IsCartExists(string cartId)
        {
            var cart = await GetById(cartId);
            return !string.IsNullOrWhiteSpace(cart.CartId);
        }
    }
}
