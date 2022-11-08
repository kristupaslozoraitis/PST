using KAMANDAX.DB;
using KAMANDAX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Services
{
    public class ProductService
    {
        private readonly WebDbContext _db;

        public ProductService(WebDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> Create(Product product)
        {
            _db.Add(product);
            await _db.SaveChangesAsync();

            return product;
        }
        public async Task<Product> GetProductByProductId(Guid id)
        {
            Product product = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == id);
            return product;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            Product product =  await _db.Products.FirstOrDefaultAsync(u => u.ProductUserId == id);
            return product;
        }
        public async Task<List<Product>> GetProductsByUserId(Guid id)
        {
            return await _db.Products.Where(u => u.ProductUserId == id).ToListAsync();
        }

        public async Task EditProduct(Product Product, Guid id)
        {
            Product oldProduct = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == id);
            oldProduct.ProductId = Product.ProductId;
            oldProduct.ProductUserId = Product.ProductUserId;
            oldProduct.Title = Product.Title;
            oldProduct.Category = Product.Category;
            oldProduct.Description = Product.Description;
            oldProduct.Price = Product.Price;
            oldProduct.AuctionType = Product.AuctionType;
            oldProduct.SellingType = Product.SellingType;
            oldProduct.ImageData = Product.ImageData;
            oldProduct.OrderedBy = Product.OrderedBy;
            oldProduct.StartDate = Product.StartDate;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
             _db.Remove(product);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAllProducts(Guid id)
        {
            _db.Products.RemoveRange(_db.Products.Where(x => x.ProductUserId == id));
            await _db.SaveChangesAsync();
        }

        public async Task UpdateProductViewedCount(Guid id)
        {
            Product product = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == id);
            product.TimesViewed = product.TimesViewed + 1;
            await _db.SaveChangesAsync();
        }

        public async Task EditOrderedBy(Guid productId, Guid orderedBy)
        {
            Product product = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == productId);
            product.OrderedBy = orderedBy;
            await _db.SaveChangesAsync();
        }

    }
}
