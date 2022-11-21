using KAMANDAX.DB;
using KAMANDAX.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly WebDbContext _db;

        public CartItemService(WebDbContext db)
        {
            _db = db;
        }

        public async Task<List<CartItem>> GetUserCartItems(Guid userId)
        {
            return await _db.CartItems.Where(i => i.ProductUserId == userId).ToListAsync();
        }

        public async Task<CartItem> Create(CartItem item)
        {
            _db.Add(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public async Task DeleteCartItem(CartItem cartItem)
        {
            _db.Remove(cartItem);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAllUserItems(Guid userid)
        {
            _db.CartItems.RemoveRange(_db.CartItems.Where(u => u.ProductUserId == userid));
            await _db.SaveChangesAsync();
        }
    }
}
