using Microsoft.AspNetCore.Mvc;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Services;

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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _orderService.CreateOrderAsync(orderDto);
            if (!success) return StatusCode(500, "Unable to create order.");
            return Ok("Order created successfully.");
        }
    }
}
