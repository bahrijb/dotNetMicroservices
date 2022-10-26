using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Cart.DataAccess.Repositories
{
    public interface ICartRepository
    {
        Task<bool> Create(Models.Cart cartToAdd);
        Task<Models.Cart> GetById(string cartId);
        Task<bool> IsCartExists(string cartId);
        Task<bool> Update(Models.Cart cart);
        Task<bool> DeleteById(string cartId);
        Task<List<Models.Cart>> GetAllCarts()
    }
}