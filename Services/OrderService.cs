using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Services
{
    public class OrderService : IOrder
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderDTO>> GetOrdersByUserIdAsync(int userId)
        {
            return await _dbContext.Orders
                .Where(o => o.User_Id == userId)
                .Include(o => o.OrderItems)
                .Select(o => new OrderDTO
                {
                    Order_Id = o.Order_Id,
                    User_Id = o.User_Id,
                    Order_Date = o.Order_Date,
                    Total_Amount = o.Total_Amount,
                    Status = o.Status,
                    CreatedAt = o.CreatedAt,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        OrderItem_Id = oi.OrderItem_Id,
                        Order_Id = oi.Order_Id,
                        Product_Id = oi.Product_Id,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<bool> CreateOrderAsync(OrderDTO orderDto)
        {
            var order = new Order
            {
                User_Id = orderDto.User_Id,
                Order_Date = orderDto.Order_Date,
                Total_Amount = orderDto.Total_Amount,
                Status = orderDto.Status,
                CreatedAt = DateTime.UtcNow,
                OrderItems = orderDto.OrderItems.Select(oi => new OrderItem
                {
                    Product_Id = oi.Product_Id,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
