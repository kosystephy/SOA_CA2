using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Enums;
using SOA_CA2_E_Commerce.Interface;
using SOA_CA2_E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOA_CA2_E_Commerce.Services
{
    public class OrderService : IOrder
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdersDTO>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .Select(o => new OrdersDTO
                {
                    Order_Id = o.Order_Id,
                    Customer_Id = o.Customer_Id,
                    Order_Date = o.Order_Date,
                    Total_Amount = o.Total_Amount,
                    Status = o.Status.ToString(), // Convert enum to string
                    Payment_Method = o.Payment_Method.ToString() // Convert enum to string
                }).ToListAsync();
        }

        public async Task<OrdersDTO> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Order_Id == id);

            if (order == null) return null;

            return new OrdersDTO
            {
                Order_Id = order.Order_Id,
                Customer_Id = order.Customer_Id,
                Order_Date = order.Order_Date,
                Total_Amount = order.Total_Amount,
                Status = order.Status.ToString(), // Convert enum to string
                Payment_Method = order.Payment_Method.ToString() // Convert enum to string
            };
        }

        public async Task CreateOrderAsync(OrdersDTO orderDTO)
        {
            // Convert strings to enums
            if (!Enum.TryParse<OrderStatus>(orderDTO.Status, true, out var parsedStatus))
            {
                throw new ArgumentException($"Invalid order status value: {orderDTO.Status}");
            }

            if (!Enum.TryParse<PaymentMethod>(orderDTO.Payment_Method, true, out var parsedPaymentMethod))
            {
                throw new ArgumentException($"Invalid payment method value: {orderDTO.Payment_Method}");
            }

            var order = new Orders
            {
                Customer_Id = orderDTO.Customer_Id,
                Order_Date = orderDTO.Order_Date,
                Total_Amount = orderDTO.Total_Amount,
                Status = parsedStatus,
                Payment_Method = parsedPaymentMethod
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(int id, OrdersDTO orderDTO)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Order_Id == id);
            if (order == null) return;

            // Convert strings to enums
            if (!Enum.TryParse<OrderStatus>(orderDTO.Status, true, out var parsedStatus))
            {
                throw new ArgumentException($"Invalid order status value: {orderDTO.Status}");
            }

            if (!Enum.TryParse<PaymentMethod>(orderDTO.Payment_Method, true, out var parsedPaymentMethod))
            {
                throw new ArgumentException($"Invalid payment method value: {orderDTO.Payment_Method}");
            }

            order.Customer_Id = orderDTO.Customer_Id;
            order.Order_Date = orderDTO.Order_Date;
            order.Total_Amount = orderDTO.Total_Amount;
            order.Status = parsedStatus;
            order.Payment_Method = parsedPaymentMethod;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Order_Id == id);
            if (order == null) return;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrdersDTO>> GetOrdersByStatusAsync(string status)
        {
            // Convert string to enum
            if (!Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
            {
                throw new ArgumentException($"Invalid order status value: {status}");
            }

            return await _context.Orders
                .Where(o => o.Status == parsedStatus)
                .Select(o => new OrdersDTO
                {
                    Order_Id = o.Order_Id,
                    Customer_Id = o.Customer_Id,
                    Order_Date = o.Order_Date,
                    Total_Amount = o.Total_Amount,
                    Status = o.Status.ToString(), // Convert enum to string
                    Payment_Method = o.Payment_Method.ToString() // Convert enum to string
                }).ToListAsync();
        }
    }
}
