using Microsoft.AspNetCore.Mvc;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;

namespace SOA_CA2_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItem _cartItemService;

        public CartItemController(ICartItem cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItems()
        {
            var cartItems = await _cartItemService.GetAllCartItems();
            return Ok(cartItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItemById(int id)
        {
            try
            {
                var cartItem = await _cartItemService.GetCartItemById(id);
                return Ok(cartItem);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDTO cartItemDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addedCartItem = await _cartItemService.AddToCart(cartItemDTO);
            return CreatedAtAction(nameof(GetCartItemById), new { id = addedCartItem.CartItem_Id }, addedCartItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItemDTO cartItemDTO)
        {
            try
            {
                var updatedCartItem = await _cartItemService.UpdateCartItem(id, cartItemDTO);
                return Ok(updatedCartItem);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                await _cartItemService.DeleteCartItem(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("cart/{cartId}")]
        public async Task<IActionResult> GetCartItemsByCartId(int cartId)
        {
            var cartItems = await _cartItemService.GetCartItemsByCartId(cartId);
            return Ok(cartItems);
        }
    }
}
