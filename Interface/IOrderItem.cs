using System.Collections.Generic;
using System.Threading.Tasks;
using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Interface
{
    public interface IOrderItem
    {
        //CRUD Methods
        Task<IEnumerable<OrderItemDTO>> GetAllOrderItems();
        Task<OrderItemDTO> GetOrderItemById(int id);
        Task<OrderItemDTO> CreateOrderItem(OrderItemDTO orderItemDTO);
        Task<OrderItemDTO> UpdateOrderItem(int id, OrderItemDTO orderItemDTO);
        Task DeleteOrderItem(int id);
        Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderId(int orderId);

    }
}
