using AutoFixture;
using KAMANDAX.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Controllers
{
    public class OrderInformationControllerTests
    {
        private Mock<IOrdersService> mockOrdersService;
        private readonly Fixture fixture;

        public OrderInformationControllerTests()
        {
            mockOrdersService = new Mock<IOrdersService>();
            fixture = new Fixture();
        }

        private OrderInformationController CreateOrderInformationController()
        {
            return new OrderInformationController(mockOrdersService.Object);
        }

        [Fact]
        public async Task AddOrderItem_StateUnderTest_ExpectedBehavior()
        {
            var orderInformationController = CreateOrderInformationController();
            OrderInformation order = fixture.Create<OrderInformation>();

            var result = await orderInformationController.AddOrderItem(order);

            mockOrdersService.Verify(x => x.Create(It.IsAny<OrderInformation>()), Times.Once);
            var okResult = Assert.IsType<OkResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
