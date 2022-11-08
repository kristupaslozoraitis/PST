using KAMANDAX.Models;
using KAMANDAX.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _products;

        public ProductController(ProductService products)
        {
            _products = products;
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct(ProductRequest product, Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Product newProduct = new Product()
            {
                ProductId = new Guid(),
                ProductUserId = userId,
                Title = product.Title,
                Category = product.Category,
                Description = product.Description,
                SellingType = product.SellingType,
                Price = product.Price,
                AuctionType = product.AuctionType,
                ImageData = product.ImageData,
                StartDate=product.StartDate,
                OrderedBy= product.ProductBuyerID,
                TimesViewed = 0
            };

            await _products.Create(newProduct);

            return Ok();
        }



        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(Product product)
        {
            await _products.DeleteProduct(product);

            return Ok();
        }

        [HttpPut("editProduct")]

        public async Task<IActionResult> EditProduct(ProductRequest product, Guid oldId, Guid userId)
        {
            Product newProduct = new Product()
            {
                ProductId = oldId,
                ProductUserId = userId,
                Title = product.Title,
                Category = product.Category,
                Description = product.Description,
                SellingType = product.SellingType,
                Price = product.Price,
                AuctionType = product.AuctionType,
                ImageData = product.ImageData,
                StartDate = product.StartDate,
                OrderedBy =product.ProductBuyerID
            };

            await _products.EditProduct(newProduct, oldId);

            return Ok();
        }


    }
}
