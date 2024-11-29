using System.Collections.Generic;
using System.Threading.Tasks;
using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Interface
{
    public interface IOrder
    {
        // CRUD Operations
        Task<IEnumerable<OrdersDTO>> GetAllOrdersAsync();
        Task<OrdersDTO> GetOrderByIdAsync(int id);
        Task CreateOrderAsync(OrdersDTO orderDTO);
        Task UpdateOrderAsync(int id, OrdersDTO orderDTO);
        Task DeleteOrderAsync(int id);

        // Custom Operation
        Task<IEnumerable<OrdersDTO>> GetOrdersByStatusAsync(string status);
    }
}
