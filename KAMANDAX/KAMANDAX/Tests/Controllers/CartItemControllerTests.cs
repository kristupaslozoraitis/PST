using AutoFixture;
using KAMANDAX.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Controllers
{
    public class CartItemControllerTests
    {
        private Mock<ICartItemService> mockCartItemService;
        private Fixture fixture;

        public CartItemControllerTests()
        {
            mockCartItemService = new Mock<ICartItemService>();
            fixture = new Fixture();
        }

        private CartItemController CreateCartItemController()
        {
            return new CartItemController(mockCartItemService.Object);
        }

        [Fact]
        public async Task AddCartItem_StateUnderTest_ExpectedBehavior()
        {
            var cartItemController = CreateCartItemController();
            Product product = fixture.Create<Product>();
            Guid userId = new Guid();

            mockCartItemService.Setup(x => x.GetUserCartItems(It.IsAny<Guid>())).ReturnsAsync(new List<CartItem>());
            var result = await cartItemController.AddCartItem(product, userId);

            mockCartItemService.Verify(x => x.Create(It.IsAny<CartItem>()), Times.Once);
            var okResult = Assert.IsType<OkResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteCartItem_StateUnderTest_ExpectedBehavior()
        {
            var cartItemController = CreateCartItemController();
            CartItem cartItem = fixture.Create<CartItem>();

            mockCartItemService.Setup(x => x.GetUserCartItems(It.IsAny<Guid>())).ReturnsAsync(new List<CartItem>());

            var result = await cartItemController.DeleteCartItem(
                cartItem);

            mockCartItemService.Verify(x => x.DeleteCartItem(It.IsAny<CartItem>()), Times.Once);
            var okResult = Assert.IsType<OkResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetUserCartItems_StateUnderTest_ExpectedBehavior()
        {
            var cartItemController = CreateCartItemController();
            Guid userId = new Guid();

            var result = await cartItemController.GetUserCartItems(userId);

            mockCartItemService.Verify(x => x.GetUserCartItems(userId), Times.Once);
        }

        [Fact]
        public async Task DeleteAllUserItems_StateUnderTest_ExpectedBehavior()
        {
            var cartItemController = this.CreateCartItemController();
            Guid userId = new Guid();

            await cartItemController.DeleteAllUserItems(userId);

            mockCartItemService.Verify(x => x.DeleteAllUserItems(userId), Times.Once);
        }
    }
}
