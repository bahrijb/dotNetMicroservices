using OrderMe.Cart.BusinessLogic.Cart.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderMe.Cart.BusinessLogic.Cart.Services
{
    public interface ICartService
    {
        Task<CartDto> Create(CartDto cartDto);
        Task<bool> Delete(string id);
        Task<List<CartDto>> GetAll();
        Task<CartDto> GetById(string cartId);
        Task<bool> Update(string id, CartDto cartDto);
    }
}