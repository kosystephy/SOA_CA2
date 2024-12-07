using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;
using SOA_CA2_E_Commerce.Services;
namespace SOA_CA2_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICart _cartService;
        public CartController(ICart cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null) return NotFound("Cart not found.");
            return Ok(cart);
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToCart(int userId, [FromBody] CartItemDTO cartItemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _cartService.AddToCartAsync(userId, cartItemDto);
            if (!success) return StatusCode(500, "Unable to add to cart.");
            return Ok("Item added to cart.");
        }
        [HttpDelete("{userId}/product/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            var success = await _cartService.RemoveFromCartAsync(userId, productId);
            if (!success) return NotFound("Item not found in cart.");
            return Ok("Item removed from cart.");
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var success = await _cartService.ClearCartAsync(userId);
            if (!success) return NotFound("Cart not found.");
            return Ok("Cart cleared.");
        }
    }
}