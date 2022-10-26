using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderMe.Cart.DataAccess.Constants;

namespace OrderMe.Cart.DataAccess.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<Models.Cart> _cartsCollection;

        public CartRepository(IOptions<CartStoreDataBaseSetings> cartStoreDatabaseSettings)
        {
            var mongoDatabase = InitalizeCartConneciton(cartStoreDatabaseSettings);
            _cartsCollection = mongoDatabase.GetCollection<DataAccess.Models.Cart>(cartStoreDatabaseSettings.Value.CartCollectionName);
        }

        public async Task<bool> Create(Models.Cart cartToAdd)
        {
            try
            {
                await _cartsCollection.InsertOneAsync(cartToAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public async Task<Models.Cart> GetById(string cartId)
        {
            var cart = await _cartsCollection.Find(x => x.CartId == cartId).FirstOrDefaultAsync();
            if (cart == null) return new Models.Cart();
            return cart;
        }

        public async Task<bool> Update(Models.Cart cart)
        {
            var cartExists = await _cartsCollection.Find(x => x.CartId == cart.CartId).AnyAsync();
            if (cartExists)
            {
                await _cartsCollection.ReplaceOneAsync(x => x.CartId == cart.CartId, cart);
                return true;
            }
            return false;
        }

        public async Task<bool> IsCartExists(string cartId)
        {
            var cart = await GetById(cartId);
            return !string.IsNullOrWhiteSpace(cart.CartId);
        }

        public async Task<bool> DeleteById(string cartId)
        {
            var cart = await _cartsCollection.Find(x => x.CartId == cartId).FirstOrDefaultAsync();
            if (cart == null) return false;
            await _cartsCollection.DeleteOneAsync(x => x.CartId == cartId);
            return false;
        }
        public async Task<List<Models.Cart>> GetAllCarts()
        {
            var carts = await _cartsCollection.Find(_ => true).ToListAsync();
            if (carts == null || !carts.Any()) return new List<Models.Cart>();
            return carts;
        }

        private IMongoDatabase InitalizeCartConneciton(IOptions<CartStoreDataBaseSetings> cartStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                cartStoreDatabaseSettings.Value.ConnectionString);

            return mongoClient.GetDatabase(
                cartStoreDatabaseSettings.Value.DatabaseName);
        }

    }
}
