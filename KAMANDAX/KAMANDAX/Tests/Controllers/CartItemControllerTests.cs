using AutoFixture;
using KAMANDAX.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Controllers
{
    public class CartItemControllerTests
    {
        private Fixture fixture;

        public CartItemControllerTests()
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

        private CartItemController CreateCartItemController(ICartItemService cartItemService)
        {
            return new CartItemController(cartItemService);
        }

        [Fact]
        public async Task AddCartItem_MultipleCartItemsWithSameProductIdExist_Status200Response()
        {
            WebDbContext context = CreateInMemoryWebDbContext();
            ICartItemService cartItemService = new CartItemService(context);
            var cartItemController = CreateCartItemController(cartItemService);
            Product product = fixture.Create<Product>();
            Guid userId = new Guid();

            var result = await cartItemController.AddCartItem(product, userId);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task AddCartItem_MultipleCartItemsWithSameProductIdExist_Status400Response()
        {
            Product product = fixture.Create<Product>();
            Guid userId = new Guid();
            CartItem firstCartItem = fixture.Create<CartItem>();
            firstCartItem.ProductId = product.ProductId;
            firstCartItem.ProductUserId = userId;
            CartItem secondCartItem = fixture.Create<CartItem>();
            secondCartItem.ProductId = product.ProductId;
            secondCartItem.ProductUserId = userId;
            WebDbContext context = CreateInMemoryWebDbContext();
            await context.CartItems.AddAsync(firstCartItem);
            await context.CartItems.AddAsync(secondCartItem);
            await context.SaveChangesAsync();
            ICartItemService cartItemService = new CartItemService(context);
            var cartItemController = CreateCartItemController(cartItemService);

            var result = await cartItemController.AddCartItem(product, userId);

            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task DeleteCartItem_ItemDeletesSuccessfully_Status200Response()
        {
            CartItem cartItem = fixture.Create<CartItem>();
            WebDbContext context = CreateInMemoryWebDbContext();
            await context.CartItems.AddAsync(cartItem);
            await context.SaveChangesAsync();
            ICartItemService cartItemService = new CartItemService(context);
            var cartItemController = CreateCartItemController(cartItemService);

            var result = await cartItemController.DeleteCartItem(cartItem);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetUserCartItems_StateUnderTest_ExpectedBehavior()
        {
            WebDbContext context = CreateInMemoryWebDbContext();
            ICartItemService cartItemService = new CartItemService(context);
            var cartItemController = CreateCartItemController(cartItemService);
            Guid userId = new Guid();

            var result = await cartItemController.GetUserCartItems(userId);
        }

        [Fact]
        public async Task DeleteAllUserItems_StateUnderTest_ExpectedBehavior()
        {
            WebDbContext context = CreateInMemoryWebDbContext();
            ICartItemService cartItemService = new CartItemService(context);
            var cartItemController = this.CreateCartItemController(cartItemService);
            Guid userId = new Guid();

            await cartItemController.DeleteAllUserItems(userId);
        }
    }
}
