using Microsoft.AspNetCore.Mvc;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Services;

namespace SOA_CA2_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;

        public ProductController(IProduct productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return NotFound("Product not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _productService.AddProductAsync(productDto);
            if (!success) return StatusCode(500, "Unable to add product.");
            return Ok("Product added successfully.");
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _productService.UpdateProductAsync(productId, productDto);
            if (!success) return NotFound("Product not found.");
            return Ok("Product updated successfully.");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var success = await _productService.DeleteProductAsync(productId);
            if (!success) return NotFound("Product not found.");
            return Ok("Product deleted successfully.");
        }
    }
}
