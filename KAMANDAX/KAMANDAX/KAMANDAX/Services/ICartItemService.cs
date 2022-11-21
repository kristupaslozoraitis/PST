using KAMANDAX.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAMANDAX.Services
{
    public interface ICartItemService
    {
        Task<CartItem> Create(CartItem item);
        Task DeleteAllUserItems(Guid userid);
        Task DeleteCartItem(CartItem cartItem);
        Task<List<CartItem>> GetUserCartItems(Guid userId);
    }
}