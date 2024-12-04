using System.Collections.Generic;
using System.Threading.Tasks;
using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Interface
{
    public interface IOrder
    {
        // CRUD Operations
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<OrderDTO> GetOrderById(int id);
        Task<OrderDTO> CreateOrder(OrderDTO orderDTO);
        Task<OrderDTO> UpdateOrder(int id, OrderDTO orderDTO);
        Task DeleteOrder(int id);
        Task<IEnumerable<OrderDTO>> GetOrdersByUserId(int userId);


    }
}
