using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Services
{
    public interface IOrder
    {
        Task<List<OrderDTO>> GetOrdersByUserIdAsync(int userId);
        Task<bool> CreateOrderAsync(OrderDTO orderDto);
    }
}
