using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KAMANDAX.Models;
using KAMANDAX.Services;
using Microsoft.AspNetCore.Mvc;

namespace KAMANDAX.Controllers
{
    public class OrderInformationController : Controller
    {
        private readonly OrdersService _orderItems;

        public OrderInformationController(OrdersService orderItems)
        {
            _orderItems = orderItems;
        }

        [HttpPost("addOrderInformation")]
        public async Task<IActionResult> AddOrderItem(OrderInformation order)
        {

            OrderInformation newOrder = new OrderInformation()
            {
                OrderID = new Guid(),
                userID = order.userID,
                EmailAddress = order.EmailAddress,
                HomeAddress = order.HomeAddress,
                FullName = order.FullName,
                City = order.City,
                Country = order.Country,
                PostalCode = order.PostalCode,
                ReceiverEmailAddress = order.ReceiverEmailAddress,
                ReceiverFullName = order.ReceiverFullName,
                ReceiverHomeAddress = order.ReceiverHomeAddress,
                ReceiverCity = order.ReceiverCity,
                ReceiverCountry = order.ReceiverCountry,
                CompanyName = order.CompanyName,
                DeliveryType = order.DeliveryType,
                DeliveryPrice = order.DeliveryPrice,
                TerminalAddress = order.TerminalAddress,
                PhoneNumber = order.PhoneNumber,
                PaymentType = order.PaymentType,
                CardHolder = order.CardHolder,
                CardNumber = order.CardNumber,
                CardMonth = order.CardMonth,
                CardYear = order.CardYear,
                CardCVC = order.CardCVC,
                TotalPrice = order.TotalPrice,
                ConfirmedOrder = order.ConfirmedOrder,
                OrderDate = order.OrderDate
                
            };


            await _orderItems.Create(newOrder);

            return Ok();
        }

        //[HttpDelete("deleteProduct")]
        //public async Task<IActionResult> DeleteCartItem(CartItem cartItem)
        //{
        //    await _cartItems.DeleteCartItem(cartItem);

        //    return Ok();
        //}
    }
}
