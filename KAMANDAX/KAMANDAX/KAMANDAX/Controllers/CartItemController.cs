using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KAMANDAX.Models;
using KAMANDAX.Services;
using Microsoft.AspNetCore.Mvc;

namespace KAMANDAX.Controllers
{
    public class CartItemController : Controller
    {

        private readonly CartItemService _cartItems;

        public CartItemController(CartItemService cartItems)
        {
            _cartItems = cartItems;
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddCartItem(Product product, Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Check to not allow one user to add multiple instances of the same product
            List<CartItem> userCartItems = await _cartItems.GetUserCartItems(userId);
            if(userCartItems.Where(i => i.ProductId == product.ProductId).Count() > 0)
            {
                return BadRequest();
            }

            CartItem cartItem = new CartItem()
            {
                Id = new Guid(),
                ProductId = product.ProductId,
                ProductUserId = userId,
                Title = product.Title,
                Category = product.Category,
                ImageData = product.ImageData,
                Price = product.Price
            };


            await _cartItems.Create(cartItem);

            return Ok();
        }

        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteCartItem(CartItem cartItem)
        {
            await _cartItems.DeleteCartItem(cartItem);

            return Ok();
        }

        public async Task<List<CartItem>> GetUserCartItems(Guid userid)
        {
            return await _cartItems.GetUserCartItems(userid);
        }

        public async Task DeleteAllUserItems(Guid userid)
        {
            await _cartItems.DeleteAllUserItems(userid);
        }
    }
}