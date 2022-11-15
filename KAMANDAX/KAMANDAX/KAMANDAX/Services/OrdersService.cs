using KAMANDAX.DB;
using KAMANDAX.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly WebDbContext _db;

        public OrdersService(WebDbContext db)
        {
            _db = db;
        }

        public async Task<List<OrderInformation>> GetOrders()
        {
            return await _db.OrderInformation.ToListAsync();
        }

        public async Task<OrderInformation> GetOrder(Guid Id)
        {
            List<OrderInformation> temp = await _db.OrderInformation.Where(x => x.userID == Id).ToListAsync();
            return temp.Last();
        }

        public async Task<List<OrderInformation>> GetOrdersByUserID(Guid Id)
        {
            return await _db.OrderInformation.Where(x => x.userID == Id).ToListAsync();
        }

        public List<string> getTerminalAddresses(OrderInformation order)
        {
            string fileName = "";
            string path = "";
            List<string> terminals = new List<string>();
            if (order.DeliveryType.Equals("LPTerminal"))
            {
                fileName = "lpexpress.csv";
                path = Path.Combine(Environment.CurrentDirectory, @"wwwroot\terminalAddresses\", fileName);
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] info = lines[i].Split(";");
                    string terminal = info[1] + " " + info[2] + " " + info[0];
                    terminals.Add(terminal);
                }
            }
            else if (order.DeliveryType.Equals("DPDTerminal"))
            {
                fileName = "dpd.csv";
                path = Path.Combine(Environment.CurrentDirectory, @"wwwroot\terminalAddresses\", fileName);
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] info = lines[i].Split(";");
                    string terminal = info[1] + " " + info[0];
                    terminals.Add(terminal);
                }
            }
            else if (order.DeliveryType.Equals("OMNIVATerminal"))
            {
                fileName = "Omniva.csv";
                path = Path.Combine(Environment.CurrentDirectory, @"wwwroot\terminalAddresses\", fileName);
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != "")
                    {
                        terminals.Add(lines[i]);
                    }
                }
            }

            return terminals;
        }

        public async Task<OrderInformation> Create(OrderInformation order)
        {
            _db.Add(order);

            await _db.SaveChangesAsync();

            return order;
        }
        public async Task UpdateOrder(OrderInformation order, Guid id)
        {
            OrderInformation oldOrder = await _db.OrderInformation.FirstOrDefaultAsync(u => u.OrderID == id);
            oldOrder.OrderID = order.OrderID;
            oldOrder.userID = order.userID;
            oldOrder.EmailAddress = order.EmailAddress;
            oldOrder.FullName = order.FullName;
            oldOrder.HomeAddress = order.HomeAddress;
            oldOrder.City = order.City;
            oldOrder.Country = order.Country;
            oldOrder.PostalCode = order.PostalCode;
            oldOrder.ReceiverEmailAddress = order.ReceiverEmailAddress;
            oldOrder.ReceiverFullName = order.ReceiverFullName;
            oldOrder.ReceiverHomeAddress = order.ReceiverHomeAddress;
            oldOrder.ReceiverCity = order.ReceiverCity;
            oldOrder.ReceiverCountry = order.ReceiverCountry;
            oldOrder.CompanyName = order.CompanyName;
            oldOrder.DeliveryType = order.DeliveryType;
            oldOrder.DeliveryPrice = order.DeliveryPrice;
            oldOrder.TerminalAddress = order.TerminalAddress;
            oldOrder.PhoneNumber = order.PhoneNumber;
            oldOrder.PaymentType = order.PaymentType;
            oldOrder.CardHolder = order.CardHolder;
            oldOrder.CardNumber = order.CardNumber;
            oldOrder.CardMonth = order.CardMonth;
            oldOrder.CardYear = order.CardYear;
            oldOrder.CardCVC = order.CardCVC;
            oldOrder.TotalPrice = order.TotalPrice;
            oldOrder.OrderDate = order.OrderDate;
            oldOrder.ConfirmedOrder = order.ConfirmedOrder;

            await _db.SaveChangesAsync();
        }
        public async Task<OrderedProducts> CreateOrderedProducts(OrderedProducts orderedProducts)
        {
            _db.Add(orderedProducts);
            await _db.SaveChangesAsync();

            return orderedProducts;
        }
        public async Task<List<OrderedProducts>> GetOrderedProducts()
        {
            return await _db.OrderedProducts.ToListAsync();
        }
    }
}
