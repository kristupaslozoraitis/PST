using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ProductServiceTests
    {
        private Fixture fixture;

        public ProductServiceTests()
        {
            fixture = new Fixture();
        }
        private static WebDbContext CreateInMemoryWebDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebDbContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new WebDbContext(optionsBuilder.Options);
            return context;
        }
        [Fact]
        public async void GetProducts_ReturnsListOfProducts()
        {
            var dbContext = CreateInMemoryWebDbContext();
            dbContext.Database.EnsureDeleted();
            var firstProduct = fixture.Create<Product>();
            var secondProduct = fixture.Create<Product>();
            dbContext.Products.Add(firstProduct);
            dbContext.Products.Add(secondProduct);
            await dbContext.SaveChangesAsync();
            var productService = new ProductService(dbContext);
            var productsList = await productService.GetProducts();
            Assert.Equal(2, productsList.Count);
            Assert.Equal(firstProduct, productsList[0]);
            Assert.Equal(secondProduct, productsList[1]);
        }

        [Fact]
        public async void CreateProduct_SuccessfulyCreated()
        {
            var dbContext = CreateInMemoryWebDbContext();
            dbContext.Database.EnsureDeleted();
            var product = fixture.Create<Product>();
            var productService = new ProductService(dbContext);
            var createdProduct = await productService.Create(product);
            Assert.Equal(product, createdProduct);
        }

        [Fact]
        public async void GetProductByProductId_ReturnProduct()
        {
            var dbContext = CreateInMemoryWebDbContext();
            dbContext.Database.EnsureDeleted();
            var firstProduct = fixture.Create<Product>();
            var secondProduct = fixture.Create<Product>();
            dbContext.Products.Add(firstProduct);
            dbContext.Products.Add(secondProduct);
            await dbContext.SaveChangesAsync();
            var productService = new ProductService(dbContext);
            var product = await productService.GetProductByProductId(secondProduct.ProductId);
            Assert.Equal(secondProduct.ProductId, product.ProductId);
        }

        [Fact]
        public async void GetProductByProductId_ReturnNull()
        {
            var dbContext = CreateInMemoryWebDbContext();
            dbContext.Database.EnsureDeleted();
            var firstProduct = fixture.Create<Product>();
            var secondProduct = fixture.Create<Product>();
            dbContext.Products.Add(firstProduct);
            dbContext.Products.Add(secondProduct);
            await dbContext.SaveChangesAsync();
            var productService = new ProductService(dbContext);
            var product = await productService.GetProductByProductId(new Guid());
            Assert.Null(product);
        }

        [Fact]
        public async void GetProductById_ReturnProduct()
        {
            var dbContext = CreateInMemoryWebDbContext();
            dbContext.Database.EnsureDeleted();
            var firstProduct = fixture.Create<Product>();
            var secondProduct = fixture.Create<Product>();
            dbContext.Products.Add(firstProduct);
            dbContext.Products.Add(secondProduct);
            await dbContext.SaveChangesAsync();
            var productService = new ProductService(dbContext);
            var product = await productService.GetProductById(secondProduct.ProductUserId);
            Assert.Equal(secondProduct.ProductId, product.ProductId);
        }

        [Fact]
        public async void GetProductById_ReturnNull()
        {
            var dbContext = CreateInMemoryWebDbContext();
            dbContext.Database.EnsureDeleted();
            var firstProduct = fixture.Create<Product>();
            var secondProduct = fixture.Create<Product>();
            dbContext.Products.Add(firstProduct);
            dbContext.Products.Add(secondProduct);
            await dbContext.SaveChangesAsync();
            var productService = new ProductService(dbContext);
            var product = await productService.GetProductById(new Guid());
            Assert.Null(product);
        }

        [Fact]
        public async void DeleteProduct_DeleteSuccessfully()
        {
            var dbContext = CreateInMemoryWebDbContext();
            dbContext.Database.EnsureDeleted();
            var firstProduct = fixture.Create<Product>();
            var secondProduct = fixture.Create<Product>();
            dbContext.Products.Add(firstProduct);
            dbContext.Products.Add(secondProduct);
            await dbContext.SaveChangesAsync();
            var productService = new ProductService(dbContext);
            await productService.DeleteProduct(firstProduct);
            var allProducts = await dbContext.Products.ToListAsync();
            Assert.Single(allProducts);
            Assert.Equal(secondProduct, allProducts[0]);
        }

        [Fact]
        public async void DeleteAllProductsByUserId_ReturnsEmptyList()
        {
            var dbContext = CreateInMemoryWebDbContext();
            dbContext.Database.EnsureDeleted();
            var firstProduct = fixture.Create<Product>();
            var secondProduct = fixture.Create<Product>();
            secondProduct.ProductUserId = firstProduct.ProductUserId;
            dbContext.Products.Add(firstProduct);
            dbContext.Products.Add(secondProduct);
            await dbContext.SaveChangesAsync();
            var productService = new ProductService(dbContext);
            await productService.DeleteAllProducts(firstProduct.ProductUserId);
            var allProducts = await dbContext.Products.ToListAsync();
            Assert.Empty(allProducts);
        }
    }
}
