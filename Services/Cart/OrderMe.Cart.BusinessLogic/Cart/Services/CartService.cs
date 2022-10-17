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

        public CartService(
            IOptions<CartStoreDataBaseSetings> cartStoreDatabaseSettings, IMapper mapper)
        {

            _mapper = mapper;

            var mongoClient = new MongoClient(
                cartStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                cartStoreDatabaseSettings.Value.DatabaseName);

            _cartsCollection = mongoDatabase.GetCollection<DataAccess.Models.Cart>(
                cartStoreDatabaseSettings.Value.CartCollectionName);
        }

        public async Task<Dtos.CartDto> Create(Dtos.CartDto cartDto)
        {
            var cartToAdd = _mapper.Map<DataAccess.Models.Cart>(cartDto);
            await _cartsCollection.InsertOneAsync(cartToAdd);
            return cartDto;
        }

        public async Task<bool> Delete(string id)
        {
            var cart = await _cartsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (cart == null) return false;
            await _cartsCollection.DeleteOneAsync(x => x.Id == id);
            return false;
        }

        public async Task<List<Dtos.CartDto>> GetAll()
        {
            var carts = await _cartsCollection.Find(_ => true).ToListAsync();
            if (carts == null || !carts.Any()) return new List<Dtos.CartDto>();
            var existingCarts = _mapper.Map<List<DataAccess.Models.Cart>, List<Dtos.CartDto>>(carts);
            return existingCarts;
        }

        public async Task<Dtos.CartDto> GetById(string cartId)
        {
            var cart = await _cartsCollection.Find(x => x.Id == cartId).FirstOrDefaultAsync();
            if (cart == null) return new Dtos.CartDto();
            var existingCart = _mapper.Map<Dtos.CartDto>(cart);
            return existingCart;
        }

        public async Task<bool> Update(string id, Dtos.CartDto cartDto)
        {
            var cartExists = await _cartsCollection.Find(x => x.Id == id).AnyAsync();
            if (cartExists)
            {
                var cart = _mapper.Map<DataAccess.Models.Cart>(cartDto);
                await _cartsCollection.ReplaceOneAsync(x => x.Id == id, cart);
                return true;
            }
            return false;
        }
    }
}
