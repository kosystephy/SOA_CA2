using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Services
{
    public class OrderService : IOrder
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            return await _context.Orders
                .Select(o => new OrderDTO
                {
                    Order_Id = o.Order_Id,
                    User_Id = o.User_Id,
                    CreatedAt = o.CreatedAt,
                    Total_Amount = o.Total_Amount,
                    Status = o.Status
                }).ToListAsync();
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) throw new KeyNotFoundException("Order not found");

            return new OrderDTO
            {
                Order_Id = order.Order_Id,
                User_Id = order.User_Id,
                CreatedAt = order.CreatedAt,
                Total_Amount = order.Total_Amount,
                Status = order.Status
            };
        }

        public async Task<OrderDTO> CreateOrder(OrderDTO orderDTO)
        {
            var order = new Order
            {
                User_Id = orderDTO.User_Id,
                CreatedAt = DateTime.UtcNow,
                Total_Amount = orderDTO.Total_Amount,
                Status = orderDTO.Status
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            orderDTO.Order_Id = order.Order_Id;
            return orderDTO;
        }

        public async Task<OrderDTO> UpdateOrder(int id, OrderDTO orderDTO)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) throw new KeyNotFoundException("Order not found");

            order.Status = orderDTO.Status;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return orderDTO;
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) throw new KeyNotFoundException("Order not found");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserId(int userId)
        {
            return await _context.Orders
                .Where(o => o.User_Id == userId)
                .Select(o => new OrderDTO
                {
                    Order_Id = o.Order_Id,
                    User_Id = o.User_Id,
                    CreatedAt = o.CreatedAt,
                    Total_Amount = o.Total_Amount,
                    Status = o.Status
                }).ToListAsync();
        }
    }
}
