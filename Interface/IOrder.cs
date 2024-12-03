using System.Collections.Generic;
using System.Threading.Tasks;
using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Interface
{
    public interface IOrder
    {
        // CRUD Operations
        Task<IEnumerable<OrdersDTO>> GetAllOrders();
        Task<OrdersDTO> GetOrderById(int id);
        Task CreateOrder(OrdersDTO orderDTO);
        Task UpdateOrder(int id, OrdersDTO orderDTO);
        Task DeleteOrder(int id);

        // Custom Operation
        Task<IEnumerable<OrdersDTO>> GetOrdersByStatus(string status);
    }
}
