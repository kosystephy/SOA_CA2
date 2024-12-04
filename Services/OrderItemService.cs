using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Services
{
    public class OrderItemService : IOrderItem
    {
        private readonly ApplicationDbContext _context;

        public OrderItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItemDTO>> GetAllOrderItems()
        {
            return await _context.OrderItems
                .Select(oi => new OrderItemDTO
                {
                    OrderItem_Id = oi.OrderItem_Id,
                    Order_Id = oi.Order_Id,
                    Product_Id = oi.Product_Id,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToListAsync();
        }

        public async Task<OrderItemDTO> GetOrderItemById(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null) throw new KeyNotFoundException("Order item not found");

            return new OrderItemDTO
            {
                OrderItem_Id = orderItem.OrderItem_Id,
                Order_Id = orderItem.Order_Id,
                Product_Id = orderItem.Product_Id,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };
        }

        public async Task<OrderItemDTO> CreateOrderItem(OrderItemDTO orderItemDTO)
        {
            var orderItem = new OrderItem
            {
                Order_Id = orderItemDTO.Order_Id,
                Product_Id = orderItemDTO.Product_Id,
                Quantity = orderItemDTO.Quantity,
                Price = orderItemDTO.Price
            };

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            orderItemDTO.OrderItem_Id = orderItem.OrderItem_Id;
            return orderItemDTO;
        }

        public async Task<OrderItemDTO> UpdateOrderItem(int id, OrderItemDTO orderItemDTO)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null) throw new KeyNotFoundException("Order item not found");

            orderItem.Quantity = orderItemDTO.Quantity;
            orderItem.Price = orderItemDTO.Price;

            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();

            return orderItemDTO;
        }

        public async Task DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null) throw new KeyNotFoundException("Order item not found");

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderId(int orderId)
        {
            return await _context.OrderItems
                .Where(oi => oi.Order_Id == orderId)
                .Select(oi => new OrderItemDTO
                {
                    OrderItem_Id = oi.OrderItem_Id,
                    Order_Id = oi.Order_Id,
                    Product_Id = oi.Product_Id,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToListAsync();
        }
    }
}
