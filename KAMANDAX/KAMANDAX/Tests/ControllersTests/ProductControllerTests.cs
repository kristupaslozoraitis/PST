using KAMANDAX.Controllers;

namespace Tests.ControllersTests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task ProductController_DeleteProduct_CallsProductServiceDelete()
        {
            var serviceMock = new Mock<IProductService>();
            var sut = new ProductController(serviceMock.Object);
            var userId = new Guid();
            var prId = new Guid();
            var res = CreateProductAndRequet(userId, prId);
            var product = res.Item2;
            var newProduct = res.Item1;

            await sut.DeleteProduct(newProduct);

            serviceMock.Verify(a => a.DeleteProduct(newProduct)); 
        }

        [Fact]
        public async Task ProductController_AddProduct_CallsProductServiceAdd()
        {
            var serviceMock = new Mock<IProductService>();
            var sut = new ProductController(serviceMock.Object);
            var userId = new Guid();
            var prId = new Guid();
            var res = CreateProductAndRequet(userId, prId);
            var product = res.Item2;
            var newProduct = res.Item1;

            await sut.AddProduct(product, userId);

            serviceMock.Verify(a => a.Create(It.IsAny<Product>()));
        }

        [Fact]
        public async Task ProductController_EditProduct_CallsProductServiceEdit()
        {
            var serviceMock = new Mock<IProductService>();
            var sut = new ProductController(serviceMock.Object);
            var userId = new Guid();
            var prId = new Guid();
            var res = CreateProductAndRequet(userId, prId);
            var product = res.Item2;
            var newProduct = res.Item1;
            await sut.EditProduct(product, prId, userId);

            serviceMock.Verify(a => a.EditProduct(It.IsAny<Product>(), prId));
        }

        private (Product, ProductRequest) CreateProductAndRequet(Guid userId, Guid prId)
        {
            var product = new ProductRequest()
            {
                Title = "title",
                Description = "description",
                Category = "ba",
                ImageData = new byte[5],

            };
            Product newProduct = new Product()
            {
                ProductId = prId,
                ProductUserId = userId,
                Title = product.Title,
                Category = product.Category,
                Description = product.Description,
                SellingType = product.SellingType,
                Price = product.Price,
                AuctionType = product.AuctionType,
                ImageData = product.ImageData,
                StartDate = product.StartDate,
                OrderedBy = product.ProductBuyerID,
                TimesViewed = 0
            };

            return (newProduct, product);
        }
    }
}
