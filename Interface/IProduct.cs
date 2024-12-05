using System.Collections.Generic;
using System.Threading.Tasks;
using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Services
{
    public interface IProduct
    {
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int productId);
        Task<bool> AddProductAsync(ProductDTO productDto);
        Task<bool> UpdateProductAsync(int productId, ProductDTO productDto);
        Task<bool> DeleteProductAsync(int productId);
    }
}

