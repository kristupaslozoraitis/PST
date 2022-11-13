using KAMANDAX.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAMANDAX.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> Create(Product product);
        Task<Product> GetProductByProductId(Guid id);
        Task<Product> GetProductById(Guid id);
        Task<List<Product>> GetProductsByUserId(Guid id);
        Task EditProduct(Product Product, Guid id);
        Task DeleteProduct(Product product);
        Task DeleteAllProducts(Guid id);
        Task UpdateProductViewedCount(Guid id);
        Task EditOrderedBy(Guid productId, Guid orderedBy);

    }
}