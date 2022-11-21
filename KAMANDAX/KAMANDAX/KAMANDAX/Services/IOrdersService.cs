using KAMANDAX.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAMANDAX.Services
{
    public interface IOrdersService
    {
        Task<OrderInformation> Create(OrderInformation order);
        Task<OrderedProducts> CreateOrderedProducts(OrderedProducts orderedProducts);
        Task<OrderInformation> GetOrder(Guid Id);
        Task<List<OrderedProducts>> GetOrderedProducts();
        Task<List<OrderInformation>> GetOrders();
        Task<List<OrderInformation>> GetOrdersByUserID(Guid Id);
        List<string> getTerminalAddresses(OrderInformation order);
        Task UpdateOrder(OrderInformation order, Guid id);
    }
}