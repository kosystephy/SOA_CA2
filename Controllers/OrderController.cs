using Microsoft.AspNetCore.Mvc;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;
using System;
using System.Threading.Tasks;

namespace SOA_CA2_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _orderService;

        public OrderController(IOrder orderService)
        {
            _orderService = orderService;
        }

        // Get all orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // Get order by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound($"Order with ID {id} not found.");
            return Ok(order);
        }

        // Create a new order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrdersDTO orderDTO)
        {
            try
            {
                await _orderService.CreateOrderAsync(orderDTO);
                return CreatedAtAction(nameof(GetOrderById), new { id = orderDTO.Order_Id }, orderDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update an existing order
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrdersDTO orderDTO)
        {
            try
            {
                await _orderService.UpdateOrderAsync(id, orderDTO);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete an order
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        // Get orders by status
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(string status)
        {
            try
            {
                var orders = await _orderService.GetOrdersByStatusAsync(status);
                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
